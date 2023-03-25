using YADBF;

internal sealed class LoginFormWidget : ILoginFormWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITextFieldWidget EmailFieldWidget { get; }
    public IPasswordFieldWidget PasswordFieldWidget { get; }
    public IButtonWidget LoginButtonWidget { get; }

    public LoginFormWidget(
        ITextFieldWidget emailFieldWidget,
        IPasswordFieldWidget passwordInputWidget,
        IButtonWidget loginButtonWidget
    ) {
        EmailFieldWidget = emailFieldWidget;
        PasswordFieldWidget = passwordInputWidget;
        LoginButtonWidget = loginButtonWidget;
    }

    public void Dispose()
    {
    }
}