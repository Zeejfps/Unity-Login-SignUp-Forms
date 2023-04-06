﻿using YADBF;

namespace Widgets
{
    public sealed class ToggleWidget : IToggleWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsOnProp { get; } = new();
        public ObservableProperty<bool> IsFocusedProperty { get; } = new();
        public ObservableProperty<bool> IsInteractableProperty { get; } = new();
    
        public void HandleClick()
        {
            IsOnProp.Toggle();
        }
    }
}