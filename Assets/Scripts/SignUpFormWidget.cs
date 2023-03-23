using YADBF;

namespace Login
{
    internal sealed class SignUpFormWidget : ISignUpFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ITextInputWidget EmailInputWidget { get; }
        public IPasswordFieldWidget PasswordFieldWidget { get; }
        public IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
        public IButtonWidget SignUpButtonWidget { get; }
        
        public SignUpFormWidget(ISignUpFlow signUpManager)
        {
            EmailInputWidget = new SignUpFormEmailInputWidget(signUpManager);
            
            var passwordInputWidget = new SignUpFormPasswordInputWidget(signUpManager);
            PasswordFieldWidget = new PasswordFieldWidget(passwordInputWidget);

            var confirmPasswordInputWidget = new SignUpFormConfirmPasswordInputWidget(signUpManager);
            ConfirmPasswordFieldWidget = new PasswordFieldWidget(confirmPasswordInputWidget);
            
            SignUpButtonWidget = new SignUpFormSignUpButton(signUpManager);
        }
    }
}