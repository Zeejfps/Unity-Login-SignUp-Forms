using System;
using System.Threading.Tasks;
using UnityEngine;
using YADBF;

namespace Login
{
    public sealed class TestSignUpForm : ISignUpForm
    {
        public event Action Submitted;
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<string> EmailProp { get; } = new();
        public EmailValidationStatus IsEmailValid { get; private set; }
        public ObservableProperty<string> UsernameProp { get; } = new();
        public ObservableProperty<string> PasswordProp { get; } = new();
        public ObservableProperty<string> ConfirmPasswordProp { get; } = new();
        public ObservableProperty<Action> SubmitActionProp { get; } = new();

        private IPopupManager PopupManager { get; }
        private IEmailValidator EmailValidator { get; }

        public TestSignUpForm(IPopupManager popupManager)
        {
            PopupManager = popupManager;
            EmailValidator = new RegexEmailValidator();
            
            EmailProp.ValueChanged += EmailProp_OnValueChanged;
            UsernameProp.ValueChanged += UsernameProp_OnValueChanged;
            PasswordProp.ValueChanged += PasswordProp_OnValueChanged;
            ConfirmPasswordProp.ValueChanged += ConfirmPassword_PropOnValueChanged;
            UpdateState();
        }

        private void UsernameProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateState();
        }

        private void EmailProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            IsEmailValid = EmailValidator.Validate(currvalue);
            UpdateState();
        }

        private void PasswordProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateState();
        }

        private void ConfirmPassword_PropOnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            var email = EmailProp.Value;
            var username = UsernameProp.Value;
            var password = PasswordProp.Value;
            var confirmPassword = ConfirmPasswordProp.Value;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) ||
                IsEmailValid != EmailValidationStatus.Valid ||
                password != confirmPassword)
            {
                SubmitActionProp.Set(null);     
            }
            else
            {
                SubmitActionProp.Set(Submit);
            }
        }

        private async void Submit()
        {
            try
            {
                IsLoadingProp.Set(true);

                var password = PasswordProp.Value;
                var confirmPassword = ConfirmPasswordProp.Value;

                if (password != confirmPassword)
                {
                    var infoPopup = new BasicInfoPopupWidget();
                    infoPopup.TitleTextProp.Set("Error");
                    infoPopup.InfoTextProp.Set("Passwords do not match");
                    await PopupManager.ShowPopupAsync(infoPopup);
                }
                else
                {
                    await Task.Delay(2000);
                    Submitted?.Invoke();
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                IsLoadingProp.Set(false);
            }
        }
    }
}