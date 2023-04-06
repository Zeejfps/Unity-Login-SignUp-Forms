using System.Collections.Generic;
using YADBF;

public sealed class FocusController : IWidgetFocusController
{
    public event FocusChangedHandler FocusChanged;

    private IInteractable m_FocusedWidget;
    public IInteractable FocusedWidget
    {
        get => m_FocusedWidget;
        private set
        {
            if (m_FocusedWidget == value)
                return;

            var prevFocusedWidget = m_FocusedWidget;
            m_FocusedWidget = value;
            
            if (prevFocusedWidget != null)
                prevFocusedWidget.IsFocusedProperty.Set(false);

            if (m_FocusedWidget != null)
                m_FocusedWidget.IsFocusedProperty.Set(true);

            FocusChanged?.Invoke(prevFocusedWidget, m_FocusedWidget);
        }
    }

    public bool CanCycle { get; set; }

    private int m_FocusedWidgetIndex;
    private readonly List<FocusListener> m_FocusListeners = new();

    public void FocusFirstWidget()
    {
        if (m_FocusListeners.Count == 0)
            return;
        
        m_FocusListeners[0].Focus();
    }

    public void FocusNext(int skip = 0)
    {
        if (m_FocusListeners.Count == 0)
            return;
        
        var nextIndex = m_FocusedWidgetIndex + 1 + skip;
        if (nextIndex >= m_FocusListeners.Count)
            nextIndex = CanCycle ? 0 : m_FocusedWidgetIndex;
        
        if (nextIndex == m_FocusedWidgetIndex)
            return;
        
        if (m_FocusListeners[nextIndex].CanBeFocused)
            m_FocusListeners[nextIndex].Focus();
        else
            FocusNext(++skip);
    }

    public void FocusPrev(int skip = 0)
    {
        if (m_FocusListeners.Count == 0)
            return;
        
        var nextIndex = m_FocusedWidgetIndex - 1 - skip;
        if (nextIndex < 0)
            nextIndex = CanCycle ? m_FocusListeners.Count - 1 : 0;
        
        if (nextIndex == m_FocusedWidgetIndex)
            return;

        var listener = m_FocusListeners[nextIndex];
        if (listener.CanBeFocused)
            listener.Focus();
        else
            FocusPrev(++skip);
    }

    public void ClearFocus()
    {
        FocusedWidget = null;
        m_FocusedWidgetIndex = -1;
    }

    public void Add(IInteractable focusable)
    {
        var index = m_FocusListeners.Count;
        m_FocusListeners.Add(new FocusListener(index, focusable, this));
    }

    public void Remove(IInteractable focusable)
    {
        if (m_FocusListeners.Count == 0)
            return;
        
        var index = -1;
        foreach (var focusListener in m_FocusListeners)
        {
            index++;
            if (focusListener.FocusableWidget == focusable)
                break;
        }

        if (index >= 0 && index < m_FocusListeners.Count)
        {
            var listener = m_FocusListeners[index];
            listener.Dispose();
            m_FocusListeners.RemoveAt(index);
        }
    }

    public bool ProcessInputEvent(InputEvent inputEvent)
    {
        if (inputEvent == InputEvent.FocusNext)
        {
            FocusNext();
            return true;
        }
        
        if (inputEvent == InputEvent.FocusPrevious)
        {
            FocusPrev();
            return true;
        }

        return false;
    }

    public void Dispose()
    {
        foreach (var focusListener in m_FocusListeners)
            focusListener.Dispose();
        m_FocusListeners.Clear();
    }
    
    private sealed class FocusListener
    {
        public int Index { get; }
        public IInteractable FocusableWidget { get; }
        private FocusController FocusController { get; }
        public bool CanBeFocused => FocusableWidget.IsInteractableProperty.Value;


        public FocusListener(int index, IInteractable focusableWidget, FocusController focusController)
        {
            Index = index;
            FocusableWidget = focusableWidget;
            FocusController = focusController;
            FocusableWidget.IsFocusedProperty.ValueChanged += IsFocusedProperty_OnValueChanged;
        }

        public void Dispose()
        {
            FocusableWidget.IsFocusedProperty.ValueChanged -= IsFocusedProperty_OnValueChanged;
            if (FocusController.FocusedWidget == FocusableWidget) Unfocus();
        }

        public void Focus()
        {
            FocusController.FocusedWidget = FocusableWidget;
            FocusController.m_FocusedWidgetIndex = Index;
        }

        public void Unfocus()
        {
            FocusController.FocusedWidget = null;
            FocusController.m_FocusedWidgetIndex = -1;
        }
        
        private void IsFocusedProperty_OnValueChanged(ObservableProperty<bool> property, bool wasFocused, bool isFocused)
        {
            if (isFocused)
            {
                Focus();
            }
            else if (FocusController.FocusedWidget == FocusableWidget)
            {
                Unfocus();
            }
        }
    }
}
