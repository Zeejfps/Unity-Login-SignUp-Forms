using YADBF;

namespace Login
{
    internal sealed class ShowPasswordToggleWidget : IToggleWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsOnProp { get; } = new();
        public ObservableProperty<bool> IsInteractable { get; }

        private ITextInputWidget PasswordInputWidget { get; }
        
        public ShowPasswordToggleWidget(ITextInputWidget textInputWidget)
        {
            PasswordInputWidget = textInputWidget;
            PasswordInputWidget.IsMaskingCharacters.ValueChanged += IsMaskingCharacters_OnValueChanged;
            IsInteractable = PasswordInputWidget.IsInteractableProp;
            
            IsOnProp.Value = !PasswordInputWidget.IsMaskingCharacters.Value;
        }

        private void IsMaskingCharacters_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
        {
            IsOnProp.Set(!currvalue);
        }

        public void HandleClick()
        {
            PasswordInputWidget.IsMaskingCharacters.Toggle();
        }
    }
}