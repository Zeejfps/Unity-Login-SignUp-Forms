using System;
using YADBF;

internal sealed class SignUpFormEmailFieldWidget : ITextFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> ErrorTextProp { get; } = new();
    public ITextInputWidget TextInputWidget { get; }

    private ISignUpForm SignUpForm { get; }
    
    public SignUpFormEmailFieldWidget(ISignUpForm signUpForm)
    {
        SignUpForm = signUpForm;
        TextInputWidget = new SignUpFormEmailInputWidget(signUpForm);
        TextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
    }
    
    private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        var result = SignUpForm.ValidateEmail();
        switch (result)
        {
            case EmailValidationResult.Valid:
                ErrorTextProp.Set(string.Empty);
                break;
            case EmailValidationResult.Invalid:
                ErrorTextProp.Set("Please enter a valid email");
                break;
            case EmailValidationResult.Empty:
                ErrorTextProp.Set("Email can not be empty");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}