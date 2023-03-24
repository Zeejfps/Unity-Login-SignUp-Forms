using YADBF;

internal sealed class LoginFormWidget : ILoginFormWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITextInputWidget EmailInputWidget { get; }
    public IPasswordFieldWidget PasswordInputWidget { get; }
    public IButtonWidget LoginButtonWidget { get; }

    public LoginFormWidget(
        ITextInputWidget emailInputWidget,
        IPasswordFieldWidget passwordInputWidget,
        IButtonWidget loginButtonWidget
    ) {
        EmailInputWidget = emailInputWidget;
        PasswordInputWidget = passwordInputWidget;
        LoginButtonWidget = loginButtonWidget;
    }

    public void Dispose()
    {
    }
}