using System;
using System.Collections.Generic;
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
        public EmailValidationStatus EmailValidationResult { get; private set; }
        public ObservableProperty<string> UsernameProp { get; } = new();
        public ObservableProperty<string> PasswordProp { get; } = new();
        public ObservableProperty<string> ConfirmPasswordProp { get; } = new();
        public ObservableProperty<Action> SubmitActionProp { get; } = new();
        public IPasswordRequirement[] PasswordRequirements { get; }
        public bool AreAllPasswordRequirementsMet { get; private set; }

        private IPopupManager PopupManager { get; }
        private IEmailValidator EmailValidator { get; }
        private IPasswordValidator PasswordValidator { get; }

        public TestSignUpForm(IPopupManager popupManager)
        {
            PopupManager = popupManager;
            
            EmailValidator = new RegexEmailValidator();
            PasswordValidator = new TestPasswordValidator(new List<IPasswordRequirement>
            {
                new MinLengthPasswordRequirement(3)
            });

            PasswordRequirements = new IPasswordRequirement[]
            {
                new MinLengthPasswordRequirement(3),
                new MinDigitsPasswordRequirement(1),
                new MinUpperCaseCharactersPasswordRequirement(1),
                new MinLowerCaseCharactersPasswordRequirement(1),
                new MinSpecialCharactersPasswordRequirement(1)
            };
            
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
            EmailValidationResult = EmailValidator.Validate(currvalue);
            UpdateState();
        }

        private void PasswordProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            var allRequirementsMet = true;
            foreach (var passwordRequirement in PasswordRequirements)
            {
                if (!passwordRequirement.Validate(currvalue))
                {
                    allRequirementsMet = false;
                    break;
                }
            }
            AreAllPasswordRequirementsMet = allRequirementsMet;
            ConfirmPasswordProp.Value = string.Empty;
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
                EmailValidationResult != EmailValidationStatus.Valid ||
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