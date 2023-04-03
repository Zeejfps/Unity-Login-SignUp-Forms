using YADBF;

namespace Tests
{
    public sealed class SignUpFormWidgetControllerDefaultState : SignUpFormWidgetControllerState
    {
        public SignUpFormWidgetControllerDefaultState(ISignUpFormWidgetController signUpFormWidgetController) : base(signUpFormWidgetController)
        {
        }

        public override void OnEntered()
        {
            SignUpFormWidgetController.IsLoading = false;
            
            EmailInputWidget.TextProp.ValueChanged += EmailInputWidget_TextProperty_OnValueChanged;
            PasswordInputWidget.TextProp.ValueChanged += PasswordInputWidget_TextProperty_OnValueChanged;
            ConfirmPasswordInputWidget.TextProp.ValueChanged += ConfirmPasswordInputWidget_TextProp_OnValueChanged;
        }

        public override void OnExited()
        {
            EmailInputWidget.TextProp.ValueChanged -= EmailInputWidget_TextProperty_OnValueChanged;
            PasswordInputWidget.TextProp.ValueChanged -= PasswordInputWidget_TextProperty_OnValueChanged;
            ConfirmPasswordInputWidget.TextProp.ValueChanged -= ConfirmPasswordInputWidget_TextProp_OnValueChanged;
        }

        private void EmailInputWidget_TextProperty_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            SignUpFormWidgetController.ValidateEmail();
        }

        private void PasswordInputWidget_TextProperty_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            SignUpFormWidgetController.ConfirmPassword = string.Empty;
            SignUpFormWidgetController.ValidatePassword();
        }

        private void ConfirmPasswordInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            SignUpFormWidgetController.ValidateConfirmPassword();
        }
    }
}