using YADBF;

internal sealed class SignUpFormWidget : ISignUpFormWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITextFieldWidget EmailFieldWidget { get; }
    public ITextFieldWidget UsernameFieldWidget { get; }
    public IPasswordFieldWidget PasswordFieldWidget { get; }
    public IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
    public IButtonWidget SignUpButtonWidget { get; }

    public SignUpFormWidget(
        ITextFieldWidget emailFieldWidget, 
        ITextFieldWidget usernameFieldWidget,
        IPasswordFieldWidget passwordFieldWidget,
        IPasswordFieldWidget confirmPasswordFieldWidget,
        IButtonWidget signUpButtonWidget
    ) {
        EmailFieldWidget = emailFieldWidget;
        UsernameFieldWidget = usernameFieldWidget;
        PasswordFieldWidget = passwordFieldWidget;
        ConfirmPasswordFieldWidget = confirmPasswordFieldWidget;
        SignUpButtonWidget = signUpButtonWidget;
    }
}