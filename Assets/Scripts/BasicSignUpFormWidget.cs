using System;
using UnityEngine;
using YADBF;

namespace Login
{
    internal sealed class BasicSignUpFormWidget : ISignUpFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ITextInputWidget EmailInputWidget { get; }
        public IPasswordInputWidget PasswordInputWidget { get; }
        public IPasswordInputWidget ConfirmPasswordInputWidget { get; }
        public IButtonWidget SignUpButtonWidget { get; }
        
        private ISignUpService SignUpService { get; }

        private ITextInputWidget PasswordTextInputWidget => PasswordInputWidget.TextInputWidget;
        
        public BasicSignUpFormWidget(ISignUpService signUpService)
        {
            SignUpService = signUpService;

            EmailInputWidget = new BasicTextInputWidget();
            PasswordInputWidget = new BasicPasswordInputWidget();
            ConfirmPasswordInputWidget = new BasicPasswordInputWidget();
            
            SignUpButtonWidget = new BasicButtonWidget();
            SignUpButtonWidget.ActionProp.Set(SignUp);
            
            EmailInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
            PasswordTextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
            ConfirmPasswordInputWidget.TextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
        }

        private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateButtonState();
        }

        private async void SignUp()
        {
            var emailInputWidget = EmailInputWidget;
            var passwordInputWidget = PasswordInputWidget.TextInputWidget;
            var confirmPasswordInputWidget = ConfirmPasswordInputWidget.TextInputWidget;
            var signUpButtonWidget = SignUpButtonWidget;
            
            try
            {
                emailInputWidget.IsInteractableProp.Set(false);
                passwordInputWidget.IsInteractableProp.Set(false);
                confirmPasswordInputWidget.IsInteractableProp.Set(false);
                signUpButtonWidget.IsInteractable.Set(false);
                
                var email = EmailInputWidget.TextProp.Value;
                var password = PasswordInputWidget.TextInputWidget.TextProp.Value;
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
            var email = EmailInputWidget.TextProp.Value;
            var password = PasswordInputWidget.TextInputWidget.TextProp.Value;
            var confirmPassword = ConfirmPasswordInputWidget.TextInputWidget.TextProp.Value;
            
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