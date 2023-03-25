using YADBF;

internal sealed class LoginFormEmailFieldWidget : ITextFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> ErrorTextProp { get; } = new(string.Empty);
    public ITextInputWidget TextInputWidget { get; }

    public LoginFormEmailFieldWidget(ILoginForm loginForm)
    {
        TextInputWidget = new LoginFormEmailTextInputWidget(loginForm);
    }
}