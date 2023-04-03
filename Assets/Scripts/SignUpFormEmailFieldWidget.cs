using System;
using YADBF;

internal sealed class SignUpFormEmailFieldWidget : ITextFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> ErrorTextProp { get; } = new();
    public ITextInputWidget TextInputWidget { get; }

    private ISignUpFormController SignUpForm { get; }
    
    public SignUpFormEmailFieldWidget(ISignUpFormController signUpForm)
    {
        SignUpForm = signUpForm;
        TextInputWidget = new SignUpFormEmailInputWidget(signUpForm);
        TextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
    }
    
    private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        // var status = SignUpForm.EmailValidationResult;
        // switch (status)
        // {
        //     case EmailValidationStatus.Valid:
        //         ErrorTextProp.Set(string.Empty);
        //         break;
        //     case EmailValidationStatus.Invalid:
        //         ErrorTextProp.Set("Please enter a valid email");
        //         break;
        //     case EmailValidationStatus.Empty:
        //         ErrorTextProp.Set("Email is required");
        //         break;
        //     default:
        //         throw new ArgumentOutOfRangeException();
        // }
    }
}