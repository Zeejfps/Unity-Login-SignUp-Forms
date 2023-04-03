public interface ISignUpFormWidget : IWidget
{
    ITextFieldWidget EmailFieldWidget { get; }
    ITextFieldWidget UsernameFieldWidget { get; }
    IPasswordFieldWidget PasswordFieldWidget { get; }
    IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
    IListWidget PasswordRequirementsListWidget { get; }
    IButtonWidget SignUpButtonWidget { get; }
}