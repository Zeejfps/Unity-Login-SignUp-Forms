using System;
using System.Threading.Tasks;
using UnityEngine;
using YADBF;

namespace Login
{
    public sealed class TestSignUpManager : ISignUpManager
    {
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<string> EmailProp { get; } = new();
        public ObservableProperty<string> PasswordProp { get; } = new();
        public ObservableProperty<string> ConfirmPasswordProp { get; } = new();
        public ObservableProperty<Action> SignUpActionProp { get; } = new();

        private IPopupManager PopupManager { get; }
        
        public TestSignUpManager(IPopupManager popupManager)
        {
            PopupManager = popupManager;
            EmailProp.ValueChanged += EmailProp_OnValueChanged;
            PasswordProp.ValueChanged += PasswordProp_OnValueChanged;
            ConfirmPasswordProp.ValueChanged += ConfirmPassword_PropOnValueChanged;
            UpdateState();
        }

        private void EmailProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
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
            var password = PasswordProp.Value;
            var confirmPassword = ConfirmPasswordProp.Value;

            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                SignUpActionProp.Set(null);     
            }
            else
            {
                SignUpActionProp.Set(SignUp);
            }
        }

        private async void SignUp()
        {
            try
            {
                IsLoadingProp.Set(true);

                var password = PasswordProp.Value;
                var confirmPassword = ConfirmPasswordProp.Value;

                if (password != confirmPassword)
                    await PopupManager.ShowInfoPopupAsync("Error", "Passwords do not match");
                else
                    await Task.Delay(2000);
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