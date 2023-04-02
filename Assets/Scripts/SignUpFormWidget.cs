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

    public SignUpFormWidget(
        ISignUpForm signUpForm, 
        ITextFieldWidget emailFieldWidget, 
        ITextFieldWidget usernameFieldWidget,
        IPasswordFieldWidget passwordFieldWidget,
        IPasswordFieldWidget confirmPasswordFieldWidget,
        IButtonWidget signUpButtonWidget
    ) {
        EmailFieldWidget = emailFieldWidget;
        UsernameFieldWidget = usernameFieldWidget;
        PasswordFieldWidget = passwordFieldWidget;
        ConfirmPasswordFieldWidget = confirmPasswordFieldWidget;
        SignUpButtonWidget = signUpButtonWidget;

        var passwordRequirements = signUpForm.PasswordRequirements;
        var passwordRequirementsCount = passwordRequirements.Length;
        PasswordRequirementWidgets = new IPasswordRequirementWidget[passwordRequirementsCount];
        for (var i = 0; i < passwordRequirementsCount; i++)
        {
            var passwordRequirement = passwordRequirements[i];
            PasswordRequirementWidgets[i] = new SignUpFormPasswordRequirementWidget(passwordRequirement);
        }
    }
}