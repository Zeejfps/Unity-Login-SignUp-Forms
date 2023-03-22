using System;
using UnityEngine;
using YADBF;

namespace Login
{
    internal sealed class SignUpFormSignUpButtonWidget : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractable { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();

        private ISignUpService SignUpService { get; }
        private ISignUpFormWidget SignUpFormWidget { get; }

        public SignUpFormSignUpButtonWidget(ISignUpService signUpService, ISignUpFormWidget signUpFormWidget)
        {
            SignUpService = signUpService;
            SignUpFormWidget = signUpFormWidget;
            ActionProp.Set(SignUp);
            
            signUpFormWidget.EmailInputWidgetProp.Value.TextProp.ValueChanged += TextProp_OnValueChanged;
            signUpFormWidget.PasswordInputWidgetProp.Value.TextProp.ValueChanged += TextProp_OnValueChanged;
            signUpFormWidget.ConfirmPasswordInputWidgetProp.Value.TextProp.ValueChanged += TextProp_OnValueChanged;
            UpdateIsInteractableState();
        }

        private async void SignUp()
        {
            try
            {
                IsInteractable.Set(false);
                
                var email = SignUpFormWidget.EmailInputWidgetProp.Value.TextProp.Value;
                var password = SignUpFormWidget.PasswordInputWidgetProp.Value.TextProp.Value;
                await SignUpService.SignUp(email, password);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                UpdateIsInteractableState();   
            }
        }

        private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateIsInteractableState();
        }

        private void UpdateIsInteractableState()
        {
            var allFieldsValid = ValidateFields();
            if (allFieldsValid)
                IsInteractable.Set(true);
            else
                IsInteractable.Set(false);
        }

        private bool ValidateFields()
        {
            var email = SignUpFormWidget.EmailInputWidgetProp.Value.TextProp.Value;
            var password = SignUpFormWidget.PasswordInputWidgetProp.Value.TextProp.Value;
            var confirmPassword = SignUpFormWidget.ConfirmPasswordInputWidgetProp.Value.TextProp.Value;
            
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