using System.Linq;
using YADBF;

internal sealed class SignUpFormWidget : ISignUpFormWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITextFieldWidget EmailFieldWidget { get; }
    public ITextFieldWidget UsernameFieldWidget { get; }
    public IPasswordFieldWidget PasswordFieldWidget { get; }
    public IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
    public IPasswordRequirementWidget[] PasswordRequirementWidgets { get; }
    public IButtonWidget SignUpButtonWidget { get; }

    public SignUpFormWidget(ISignUpService signUpService) {
        EmailFieldWidget = new TextFieldWidget();
        UsernameFieldWidget = new TextFieldWidget();
        PasswordFieldWidget = new PasswordFieldWidget();
        ConfirmPasswordFieldWidget = new PasswordFieldWidget();
        SignUpButtonWidget = new ButtonWidget();

        var passwordRequirements = signUpService.GetPasswordRequirements().ToArray();
        var passwordRequirementsCount = passwordRequirements.Length;
        PasswordRequirementWidgets = new IPasswordRequirementWidget[passwordRequirementsCount];
        for (var i = 0; i < passwordRequirementsCount; i++)
        {
            var passwordRequirement = passwordRequirements[i];
            PasswordRequirementWidgets[i] = new SignUpFormPasswordRequirementWidget(PasswordFieldWidget.TextInputWidget, passwordRequirement);
        }
    }
}