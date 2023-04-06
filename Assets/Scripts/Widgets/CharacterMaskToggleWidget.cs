using System;
using YADBF;

namespace Widgets
{
    internal sealed class CharacterMaskToggleWidget : IToggleWidget
    {
        public event Action<IToggleWidget> Clicked;
        
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsOnProp { get; } = new();
        public ObservableProperty<bool> IsFocusedProperty { get; } = new();
        public ObservableProperty<bool> IsInteractableProperty { get; } = new();

        private ITextInputWidget TextInputWidget { get; }
        
        public CharacterMaskToggleWidget(ITextInputWidget textInputWidget)
        {
            TextInputWidget = textInputWidget;
            IsInteractableProperty.Bind(TextInputWidget.IsInteractableProperty);
            IsOnProp.Bind(TextInputWidget.IsMaskingCharactersProperty);
        }

        public void HandleClick()
        {
            TextInputWidget.IsMaskingCharactersProperty.Toggle();
            TextInputWidget.IsFocusedProperty.Set(true);
            Clicked?.Invoke(this);
        }
    }
}