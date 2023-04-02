using Login;
using YADBF;

public sealed class SignUpFormPasswordFieldWidget : IPasswordFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);

    public ITextInputWidget TextInputWidget { get; }

    public IToggleWidget ShowPasswordToggleWidget { get; }

    public ObservableProperty<string> ErrorTextProperty { get; } = new();
    
    private ISignUpForm SignUpForm { get; }

    public SignUpFormPasswordFieldWidget(ISignUpForm signUpForm)
    {
        SignUpForm = signUpForm;
        TextInputWidget = new SignUpFormPasswordInputWidget(signUpForm);
        ShowPasswordToggleWidget = new CharacterMaskToggleWidget(TextInputWidget);
        
        TextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
    }
    
    private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        var validationResult = SignUpForm.PasswordValidationResult;
        if (validationResult.FailedRequirement != null)
        {
            ErrorTextProperty.Set(validationResult.FailedRequirement.Description);
            return;
        }
        
        ErrorTextProperty.Set(string.Empty);
    }
}