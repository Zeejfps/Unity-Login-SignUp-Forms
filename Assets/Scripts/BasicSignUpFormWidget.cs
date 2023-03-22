using System;
using UnityEngine;
using YADBF;

namespace Login
{
    internal sealed class BasicSignUpFormWidget : ISignUpFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ObservableProperty<ITextInputWidget> EmailInputWidgetProp { get; } = new();
        public ObservableProperty<IPasswordInputWidget> PasswordInputWidgetProp { get; } = new();
        public ObservableProperty<IPasswordInputWidget> ConfirmPasswordInputWidgetProp { get; } = new();
        public IButtonWidget SignUpButtonWidget { get; }
        
        private ISignUpService SignUpService { get; }

        private ITextInputWidget PasswordTextInputWidget => PasswordInputWidgetProp.Value.TextInputWidget;
        
        public BasicSignUpFormWidget(ISignUpService signUpService)
        {
            SignUpService = signUpService;
            
            EmailInputWidgetProp.Set(new BasicTextInputWidget());
            PasswordInputWidgetProp.Set(new BasicPasswordInputWidget());
            ConfirmPasswordInputWidgetProp.Set(new BasicPasswordInputWidget());
            
            SignUpButtonWidget = new BasicButtonWidget();
            SignUpButtonWidget.ActionProp.Set(SignUp);
            
            EmailInputWidgetProp.Value.TextProp.ValueChanged += TextProp_OnValueChanged;
            PasswordTextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
            ConfirmPasswordInputWidgetProp.Value.TextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
        }

        private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateButtonState();
        }

        private async void SignUp()
        {
            var emailInputWidget = EmailInputWidgetProp.Value;
            var passwordInputWidget = PasswordInputWidgetProp.Value.TextInputWidget;
            var confirmPasswordInputWidget = ConfirmPasswordInputWidgetProp.Value.TextInputWidget;
            var signUpButtonWidget = SignUpButtonWidget;
            
            try
            {
                emailInputWidget.IsInteractableProp.Set(false);
                passwordInputWidget.IsInteractableProp.Set(false);
                confirmPasswordInputWidget.IsInteractableProp.Set(false);
                signUpButtonWidget.IsInteractable.Set(false);
                
                var email = EmailInputWidgetProp.Value.TextProp.Value;
                var password = PasswordInputWidgetProp.Value.TextInputWidget.TextProp.Value;
                await SignUpService.SignUp(email, password);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                emailInputWidget.IsInteractableProp.Set(true);
                passwordInputWidget.IsInteractableProp.Set(true);
                confirmPasswordInputWidget.IsInteractableProp.Set(true);
                UpdateButtonState();   
            }
        }
        
        private void UpdateButtonState()
        {
            var signUpButtonWidget = SignUpButtonWidget;
            var allFieldsValid = ValidateFields();
            if (allFieldsValid)
                signUpButtonWidget.IsInteractable.Set(true);
            else
                signUpButtonWidget.IsInteractable.Set(false);
        }
        
        private bool ValidateFields()
        {
            var email = EmailInputWidgetProp.Value.TextProp.Value;
            var password = PasswordInputWidgetProp.Value.TextInputWidget.TextProp.Value;
            var confirmPassword = ConfirmPasswordInputWidgetProp.Value.TextInputWidget.TextProp.Value;
            
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (string.IsNullOrWhiteSpace(confirmPassword))
                return false;
            
            return true;
        }
    }
}