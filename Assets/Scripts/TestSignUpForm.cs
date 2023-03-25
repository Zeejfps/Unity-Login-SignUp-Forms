using System;
using System.Threading.Tasks;
using UnityEngine;
using YADBF;

namespace Login
{
    public sealed class TestSignUpForm : ISignUpForm
    {
        public event Action Completed;
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<string> EmailProp { get; } = new();
        public ObservableProperty<string> PasswordProp { get; } = new();
        public ObservableProperty<string> ConfirmPasswordProp { get; } = new();
        public ObservableProperty<Action> SubmitActionProp { get; } = new();

        private IPopupManager PopupManager { get; }
        
        public TestSignUpForm(IPopupManager popupManager)
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
                SubmitActionProp.Set(null);     
            }
            else
            {
                SubmitActionProp.Set(SignUp);
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
                {
                    await PopupManager.ShowInfoPopupAsync("Error", "Passwords do not match");
                }
                else
                {
                    await Task.Delay(2000);
                    var confirmationFlow = new TestSignUpConfirmationFlow();
                    confirmationFlow.Completed += SignUpConfirmationFlow_OnCompleted;
                    confirmationFlow.Canceled += SignUpConfirmationFlow_OnCanceled;
                    await PopupManager.ShowPopupAsync(new ConfirmSignUpPopupWidget(confirmationFlow));
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

        private void SignUpConfirmationFlow_OnCompleted(ISignUpConfirmationFlow flow)
        {
            flow.Completed -= SignUpConfirmationFlow_OnCompleted;
            flow.Canceled -= SignUpConfirmationFlow_OnCanceled;
            Completed?.Invoke();
        }

        private void SignUpConfirmationFlow_OnCanceled(ISignUpConfirmationFlow flow)
        {
            flow.Completed -= SignUpConfirmationFlow_OnCompleted;
            flow.Canceled -= SignUpConfirmationFlow_OnCanceled;
        }
    }
}