using System;
using YADBF;

internal sealed class LoginFormEmailFieldWidget : ITextFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> ErrorTextProp { get; } = new(string.Empty);
    public ITextInputWidget TextInputWidget { get; }

    private ILoginFormWidgetController LoginFormWidgetController { get; }
    
    public LoginFormEmailFieldWidget(ILoginFormWidgetController loginFormWidgetController)
    {
        LoginFormWidgetController = loginFormWidgetController;
        TextInputWidget = new LoginFormEmailTextInputWidget(loginFormWidgetController);
        TextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
    }
    
    private void Validate()
    {
        // var status = LoginFormWidgetController.IsEmailValid;
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

    private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        Validate();
    }
}