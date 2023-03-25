using System;
using System.Globalization;
using System.Text.RegularExpressions;
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
        public ObservableProperty<string> UsernameProp { get; } = new();
        public ObservableProperty<string> PasswordProp { get; } = new();
        public ObservableProperty<string> ConfirmPasswordProp { get; } = new();
        public ObservableProperty<Action> SubmitActionProp { get; } = new();

        private IPopupManager PopupManager { get; }

        public TestSignUpForm(IPopupManager popupManager)
        {
            PopupManager = popupManager;
            
            EmailProp.ValueChanged += EmailProp_OnValueChanged;
            UsernameProp.ValueChanged += UsernameProp_OnValueChanged;
            PasswordProp.ValueChanged += PasswordProp_OnValueChanged;
            ConfirmPasswordProp.ValueChanged += ConfirmPassword_PropOnValueChanged;
            UpdateState();
        }

        public EmailValidationResult ValidateEmail()
        {
            var email = EmailProp.Value;
            if (string.IsNullOrWhiteSpace(email))
                return EmailValidationResult.Empty;
            
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return EmailValidationResult.Invalid;
            }
            catch (ArgumentException)
            {
                return EmailValidationResult.Invalid;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))
                    ? EmailValidationResult.Valid
                    : EmailValidationResult.Invalid;
            }
            catch (RegexMatchTimeoutException)
            {
                return EmailValidationResult.Invalid;
            }
        }

        private void UsernameProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
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
            var username = UsernameProp.Value;
            var password = PasswordProp.Value;
            var confirmPassword = ConfirmPasswordProp.Value;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
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