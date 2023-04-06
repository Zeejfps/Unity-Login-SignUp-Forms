public interface ISignUpFormWidget : IWidget
{
    ITextFieldWidget EmailFieldWidget { get; }
    ITextFieldWidget UsernameFieldWidget { get; }
    IPasswordFieldWidget PasswordFieldWidget { get; }
    IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
    IListWidget<IPasswordRequirementWidget> PasswordRequirementsListWidget { get; }
    IButtonWidget SignUpButtonWidget { get; }
}