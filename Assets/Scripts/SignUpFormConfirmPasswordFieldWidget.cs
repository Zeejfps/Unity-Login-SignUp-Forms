using Login;
using YADBF;

internal sealed class SignUpFormConfirmPasswordFieldWidget : IPasswordFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ITextInputWidget TextInputWidget { get; }
    public IToggleWidget ShowPasswordToggleWidget { get; }
    public ObservableProperty<string> ErrorTextProperty { get; } = new();

    private ISignUpFormWidgetController SignUpFormWidget { get; }
    
    public SignUpFormConfirmPasswordFieldWidget(ISignUpFormWidgetController signUpFormWidget)
    {
        SignUpFormWidget = signUpFormWidget;
        TextInputWidget = new SignUpFormConfirmPasswordInputWidget(signUpFormWidget);
        ShowPasswordToggleWidget = new CharacterMaskToggleWidget(TextInputWidget);
        TextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
    }

    private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        // var password = SignUpForm.PasswordProp.Value;
        // var confirmPassword = SignUpForm.ConfirmPasswordProp.Value;
        //
        // var isMatching = password == confirmPassword;
        // if (isMatching)
        //     ErrorTextProperty.Set(string.Empty);
        // else
        //     ErrorTextProperty.Set("Passwords do not match");
    }
}