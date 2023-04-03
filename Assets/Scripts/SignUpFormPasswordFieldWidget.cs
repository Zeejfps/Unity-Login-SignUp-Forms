using Login;
using YADBF;

public sealed class SignUpFormPasswordFieldWidget : IPasswordFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);

    public ITextInputWidget TextInputWidget { get; }

    public IToggleWidget ShowPasswordToggleWidget { get; }

    public ObservableProperty<string> ErrorTextProperty { get; } = new();
    
    private ISignUpFormWidgetController SignUpFormWidget { get; }

    public SignUpFormPasswordFieldWidget(ISignUpFormWidgetController signUpFormWidget)
    {
        SignUpFormWidget = signUpFormWidget;
        TextInputWidget = new SignUpFormPasswordInputWidget(signUpFormWidget);
        ShowPasswordToggleWidget = new CharacterMaskToggleWidget(TextInputWidget);
        
        // SignUpForm.PasswordProp.ValueChanged += PasswordProp_OnValueChanged;
    }

    private void PasswordProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        // if (string.IsNullOrWhiteSpace(currvalue))
        // {
        //     ErrorTextProperty.Set("Password is required");
        //     return;
        // }
        //
        // if (!SignUpForm.AreAllPasswordRequirementsMet)
        // {
        //     ErrorTextProperty.Set("Password requirements are not met");
        //     return;
        // }
        //
        // ErrorTextProperty.Set(string.Empty);
    }
}