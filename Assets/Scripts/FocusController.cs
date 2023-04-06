using System.Collections.Generic;
using YADBF;

public sealed class FocusController : IFocusController
{
    public event FocusChangedHandler FocusChanged;

    private IFocusable m_FocusedWidget;
    public IFocusable FocusedWidget
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

    public void FocusNext()
    {
        if (m_FocusListeners.Count == 0)
            return;
        
        var nextIndex = m_FocusedWidgetIndex + 1;
        if (nextIndex >= m_FocusListeners.Count)
            nextIndex = CanCycle ? 0 : m_FocusedWidgetIndex;
        m_FocusListeners[nextIndex].Focus();
    }

    public void FocusPrev()
    {
        if (m_FocusListeners.Count == 0)
            return;
        
        var nextIndex = m_FocusedWidgetIndex - 1;
        if (nextIndex < 0)
            nextIndex = CanCycle ? m_FocusListeners.Count - 1 : 0;
        m_FocusListeners[nextIndex].Focus();
    }

    public void ClearFocus()
    {
        FocusedWidget = null;
        m_FocusedWidgetIndex = -1;
    }

    public void Add(IFocusable focusable)
    {
        var index = m_FocusListeners.Count;
        m_FocusListeners.Add(new FocusListener(index, focusable, this));
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
        public IFocusable FocusableWidget { get; }
        private FocusController FocusController { get; }


        public FocusListener(int index, IFocusable focusableWidget, FocusController focusController)
        {
            Index = index;
            FocusableWidget = focusableWidget;
            FocusController = focusController;
            FocusableWidget.IsFocusedProperty.ValueChanged += IsFocusedProperty_OnValueChanged;
        }

        public void Dispose()
        {
            FocusableWidget.IsFocusedProperty.ValueChanged -= IsFocusedProperty_OnValueChanged;
        }

        public void Focus()
        {
            FocusController.FocusedWidget = FocusableWidget;
            FocusController.m_FocusedWidgetIndex = Index;
        }
        
        private void IsFocusedProperty_OnValueChanged(ObservableProperty<bool> property, bool wasFocused, bool isFocused)
        {
            if (isFocused)
            {
                Focus();
            }
            else if (FocusController.FocusedWidget == FocusableWidget)
            {
                FocusController.FocusedWidget = null;
                FocusController.m_FocusedWidgetIndex = -1;
            }
        }
    }
}
