using YADBF;

internal sealed class LoginFormWidget : ILoginFormWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITextFieldWidget EmailFieldWidget { get; }
    public IPasswordFieldWidget PasswordFieldWidget { get; }
    public IButtonWidget LoginButtonWidget { get; }
    public IToggleWidget RememberMeToggleWidget { get; }

    public LoginFormWidget() {
        EmailFieldWidget = new TextFieldWidget();
        PasswordFieldWidget = new PasswordFieldWidget();
        LoginButtonWidget = new ButtonWidget();
        RememberMeToggleWidget = new ToggleWidget();
    }
}