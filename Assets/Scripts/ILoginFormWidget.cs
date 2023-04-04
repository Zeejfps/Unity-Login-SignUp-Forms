public interface ILoginFormWidget : IWidget
{
    ITextFieldWidget EmailFieldWidget { get; }
    IPasswordFieldWidget PasswordFieldWidget { get; }
    IButtonWidget SubmitButtonWidget { get; }
    IToggleWidget RememberMeToggleWidget { get; }
}