public interface ISignUpFormWidget : IWidget
{
    ITextInputWidget EmailInputWidget { get; }
    ITextInputWidget UsernameInputWidget { get; }
    IPasswordFieldWidget PasswordFieldWidget { get; }
    IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
    IButtonWidget SignUpButtonWidget { get; }
}