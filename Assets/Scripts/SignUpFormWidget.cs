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

        public SignUpFormWidget(
            ITextInputWidget emailInputWidget, 
            IPasswordFieldWidget passwordFieldWidget,
            IPasswordFieldWidget confirmPasswordFieldWidget,
            IButtonWidget signUpButtonWidget
        ) {
            EmailInputWidget = emailInputWidget;
            PasswordFieldWidget = passwordFieldWidget;
            ConfirmPasswordFieldWidget = confirmPasswordFieldWidget;
            SignUpButtonWidget = signUpButtonWidget;
        }
    }
}