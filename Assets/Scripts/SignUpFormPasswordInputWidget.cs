using YADBF;

namespace Login
{
    internal sealed class SignUpFormPasswordInputWidget : IPasswordInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ITextInputWidget TextInputWidget { get; }
        public IToggleWidget ShowPasswordToggleWidget { get; }
        
        public SignUpFormPasswordInputWidget(ISignUpManager signUpManager)
        {
            TextInputWidget = new StringPropTextInputWidget(signUpManager.PasswordProp);
            TextInputWidget.IsMaskingCharacters.Set(true);
            TextInputWidget.TextProp.Bind(signUpManager.PasswordProp);
            TextInputWidget.IsInteractableProp.Bind(signUpManager.IsLoadingProp, value => !value);
            ShowPasswordToggleWidget = new ShowPasswordToggleWidget(TextInputWidget);
        }

    }
}