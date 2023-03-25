public interface ILoginFormWidget : IWidget
{
    ITextInputWidget EmailInputWidget { get; }
    IPasswordFieldWidget PasswordInputWidget { get; }
    IButtonWidget LoginButtonWidget { get; }
}