public interface ILoginFormWidget : IWidget
{
    ITextFieldWidget EmailFieldWidget { get; }
    IPasswordFieldWidget PasswordFieldWidget { get; }
    IButtonWidget LoginButtonWidget { get; }
    IToggleWidget RememberMeToggleWidget { get; }
}