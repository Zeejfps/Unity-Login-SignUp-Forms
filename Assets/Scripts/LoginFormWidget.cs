using YADBF;

internal sealed class LoginFormWidget : ILoginFormWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITextFieldWidget EmailFieldWidget { get; }
    public IPasswordFieldWidget PasswordFieldWidget { get; }
    public IButtonWidget LoginButtonWidget { get; }
    public IToggleWidget RememberMeToggleWidget { get; }

    public LoginFormWidget(
        ILoginForm loginForm,
        IPasswordFieldWidget passwordInputWidget,
        IButtonWidget loginButtonWidget
    ) {
        EmailFieldWidget = new LoginFormEmailFieldWidget(loginForm);
        PasswordFieldWidget = passwordInputWidget;
        LoginButtonWidget = loginButtonWidget;
        RememberMeToggleWidget = new LoginFormRememberMeToggleWidget(loginForm);
    }
}