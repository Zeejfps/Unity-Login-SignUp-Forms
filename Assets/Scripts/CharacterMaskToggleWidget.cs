using YADBF;

namespace Login
{
    internal sealed class CharacterMaskToggleWidget : IToggleWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsOnProp { get; } = new();
        public ObservableProperty<bool> IsInteractable { get; } = new();

        private ITextInputWidget TextInputWidget { get; }
        
        public CharacterMaskToggleWidget(ITextInputWidget textInputWidget)
        {
            TextInputWidget = textInputWidget;
            IsInteractable.Bind(TextInputWidget.IsInteractableProp);
            IsOnProp.Bind(TextInputWidget.IsMaskingCharacters);
        }

        public void HandleClick()
        {
            TextInputWidget.IsMaskingCharacters.Toggle();
        }
    }
}