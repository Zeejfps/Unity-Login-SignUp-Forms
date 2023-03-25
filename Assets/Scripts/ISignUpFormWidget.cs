public interface ISignUpFormWidget : IWidget
{
    ITextFieldWidget EmailFieldWidget { get; }
    ITextFieldWidget UsernameFieldWidget { get; }
    IPasswordFieldWidget PasswordFieldWidget { get; }
    IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
    IButtonWidget SignUpButtonWidget { get; }
}