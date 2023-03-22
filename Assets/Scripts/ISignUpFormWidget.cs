using YADBF;

public interface ISignUpFormWidget : IWidget
{
    ITextInputWidget EmailInputWidget { get; }
    IPasswordFieldWidget PasswordFieldWidget { get; }
    IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
    IButtonWidget SignUpButtonWidget { get; }
}