using YADBF;

public interface ISignUpFormWidget : IWidget
{
    ITextInputWidget EmailInputWidget { get; }
    IPasswordInputWidget PasswordInputWidget { get; }
    IPasswordInputWidget ConfirmPasswordInputWidget { get; }
    IButtonWidget SignUpButtonWidget { get; }
}