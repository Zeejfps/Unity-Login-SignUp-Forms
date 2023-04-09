using System.Collections.Generic;
using YADBF;

namespace Common.Controllers
{
    public sealed class FocusGroup : IFocusGroup
    {
        public event FocusChangedHandler FocusChanged;

        private IInteractableWidget m_FocusedWidget;
        public IInteractableWidget FocusedWidget
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
        
            var nextIndex = (m_FocusedWidgetIndex + 1 + skip) % m_FocusListeners.Count;
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
        
            var nextIndex = (m_FocusedWidgetIndex - 1 - skip) % m_FocusListeners.Count;
            if (nextIndex < 0)
                nextIndex = m_FocusListeners.Count + nextIndex;
        
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

        public void Add(IInteractableWidget focusable)
        {
            var index = m_FocusListeners.Count;
            m_FocusListeners.Add(new FocusListener(index, focusable, this));
        }

        public void Remove(IInteractableWidget focusable)
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
            public IInteractableWidget FocusableWidget { get; }
            private FocusGroup FocusGroup { get; }
            public bool CanBeFocused => FocusableWidget.IsInteractableProperty.Value;
        
            public FocusListener(int index, IInteractableWidget focusableWidget, FocusGroup focusGroup)
            {
                Index = index;
                FocusableWidget = focusableWidget;
                FocusGroup = focusGroup;
                FocusableWidget.IsFocusedProperty.ValueChanged += IsFocusedProperty_OnValueChanged;
            }

            public void Dispose()
            {
                FocusableWidget.IsFocusedProperty.ValueChanged -= IsFocusedProperty_OnValueChanged;
                if (FocusGroup.FocusedWidget == FocusableWidget) Unfocus();
            }

            public void Focus()
            {
                FocusGroup.FocusedWidget = FocusableWidget;
                FocusGroup.m_FocusedWidgetIndex = Index;
            }

            public void Unfocus()
            {
                FocusGroup.FocusedWidget = null;
                FocusGroup.m_FocusedWidgetIndex = -1;
            }
        
            private void IsFocusedProperty_OnValueChanged(ObservableProperty<bool> property, bool wasFocused, bool isFocused)
            {
                if (isFocused)
                {
                    Focus();
                }
                else if (FocusGroup.FocusedWidget == FocusableWidget)
                {
                    Unfocus();
                }
            }
        }
    }
}
