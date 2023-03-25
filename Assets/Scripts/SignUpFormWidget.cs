using YADBF;

internal sealed class SignUpFormWidget : ISignUpFormWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITextInputWidget EmailInputWidget { get; }
    public ITextInputWidget UsernameInputWidget { get; }
    public IPasswordFieldWidget PasswordFieldWidget { get; }
    public IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
    public IButtonWidget SignUpButtonWidget { get; }

    public SignUpFormWidget(
        ITextInputWidget emailInputWidget, 
        ITextInputWidget usernameInputWidget,
        IPasswordFieldWidget passwordFieldWidget,
        IPasswordFieldWidget confirmPasswordFieldWidget,
        IButtonWidget signUpButtonWidget
    ) {
        EmailInputWidget = emailInputWidget;
        UsernameInputWidget = usernameInputWidget;
        PasswordFieldWidget = passwordFieldWidget;
        ConfirmPasswordFieldWidget = confirmPasswordFieldWidget;
        SignUpButtonWidget = signUpButtonWidget;
    }
}