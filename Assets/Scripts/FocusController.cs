using System.Collections.Generic;
using UnityEngine;
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

    private readonly List<FocusListener> m_FocusListeners = new();

    public void FocusFirstWidget()
    {
        if (m_FocusListeners.Count == 0)
            return;
        
        FocusedWidget = m_FocusListeners[0].FocusableWidget;
    }

    public void FocusNext()
    {
        
    }

    public void FocusPrev()
    {
    }

    public void ClearFocus()
    {
        FocusedWidget = null;
    }

    public void Add(IFocusable focusable)
    {
        m_FocusListeners.Add(new FocusListener(focusable, this));
    }

    public void Dispose()
    {
        foreach (var focusListener in m_FocusListeners)
            focusListener.Dispose();
        m_FocusListeners.Clear();
    }
    
    private sealed class FocusListener
    {
        public IFocusable FocusableWidget { get; }
        private FocusController FocusController { get; }

        public FocusListener(IFocusable focusableWidget, FocusController focusController)
        {
            FocusableWidget = focusableWidget;
            FocusController = focusController;
            FocusableWidget.IsFocusedProperty.ValueChanged += IsFocusedProperty_OnValueChanged;
        }

        public void Dispose()
        {
            FocusableWidget.IsFocusedProperty.ValueChanged -= IsFocusedProperty_OnValueChanged;
        }
        
        private void IsFocusedProperty_OnValueChanged(ObservableProperty<bool> property, bool wasFocused, bool isFocused)
        {
            if (isFocused)
            {
                FocusController.FocusedWidget = FocusableWidget;
            }
            else if (FocusController.FocusedWidget == FocusableWidget)
            {
                FocusController.FocusedWidget = null;
            }
        }
    }
}
