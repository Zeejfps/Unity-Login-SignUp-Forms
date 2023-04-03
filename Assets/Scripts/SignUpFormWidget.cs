using YADBF;

internal sealed class SignUpFormWidget : ISignUpFormWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITextFieldWidget EmailFieldWidget { get; }
    public ITextFieldWidget UsernameFieldWidget { get; }
    public IPasswordFieldWidget PasswordFieldWidget { get; }
    public IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
    public IListWidget PasswordRequirementsListWidget { get; }
    public IButtonWidget SignUpButtonWidget { get; }

    public SignUpFormWidget() {
        EmailFieldWidget = new TextFieldWidget();
        UsernameFieldWidget = new TextFieldWidget();
        PasswordFieldWidget = new PasswordFieldWidget();
        ConfirmPasswordFieldWidget = new PasswordFieldWidget();
        PasswordRequirementsListWidget = new ListWidget();
        SignUpButtonWidget = new ButtonWidget();
    }
}