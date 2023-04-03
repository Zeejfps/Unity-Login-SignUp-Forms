using System;
using System.Linq;
using UnityEngine;
using YADBF;

namespace Tests
{
    public sealed class SignUpFormWidgetController : ISignUpFormWidgetController
    {
        public event Action Submitted;

        private IPasswordRequirement[] PasswordRequirements { get; }
        private IEmailValidator EmailValidator { get; }
        private ISignUpService SignUpService { get; }
        public ISignUpFormWidget SignUpFormWidget { get; }
        public IStateMachine StateMachine { get; }

        private string Email => SignUpFormWidget.EmailFieldWidget.TextInputWidget.TextProp.Value;
        private string Username => SignUpFormWidget.UsernameFieldWidget.TextInputWidget.TextProp.Value;
        private string Password => SignUpFormWidget.PasswordFieldWidget.TextInputWidget.TextProp.Value;
        
        private EmailValidationStatus EmailValidationResult { get; set; }

        private string ConfirmPassword
        {
            get => SignUpFormWidget.ConfirmPasswordFieldWidget.TextInputWidget.TextProp.Value;

            set => SignUpFormWidget.ConfirmPasswordFieldWidget.TextInputWidget.TextProp.Value = value;
        }

        private bool AreAllPasswordRequirementsMet { get; set; }
        
        public SignUpFormWidgetController(ISignUpService signUpService, ISignUpFormWidget signUpFormWidget)
        {
            SignUpService = signUpService;
            SignUpFormWidget = signUpFormWidget;
            
            EmailValidator = new RegexEmailValidator();
            StateMachine = new SimpleStateMachine();
            PasswordRequirements = SignUpService.GetPasswordRequirements().ToArray();

            StateMachine.State = new SignUpFormWidgetControllerDefaultState(this);
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
            UpdatePasswordValidationState();
            ConfirmPassword = string.Empty;
            UpdateState();
        }

        private void ConfirmPassword_PropOnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateState();
        }

        private void UpdatePasswordValidationState()
        {
            var allRequirementsMet = true;
            foreach (var passwordRequirement in PasswordRequirements)
            {
                if (!passwordRequirement.Validate(Password))
                {
                    allRequirementsMet = false;
                    break;
                }
            }
            AreAllPasswordRequirementsMet = allRequirementsMet;
        }

        private void UpdateState()
        {
            var email = Email;
            var username = Username;
            var password = Password;
            var confirmPassword = ConfirmPassword;
            var areAllPasswordRequirementsMet = AreAllPasswordRequirementsMet;

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) ||
                !areAllPasswordRequirementsMet ||
                EmailValidationResult != EmailValidationStatus.Valid ||
                password != confirmPassword)
            {
                EnableSubmitButton();    
            }
            else
            {
                DisableSubmitButton();
            }
        }

        private void EnableSubmitButton()
        {
            SignUpFormWidget.SignUpButtonWidget.IsInteractableProp.Set(true);
        }

        private void DisableSubmitButton()
        {
            SignUpFormWidget.SignUpButtonWidget.IsInteractableProp.Set(false);
        }

        private async void Submit()
        {
            try
            {
                //IsLoadingProp.Set(true);
                await SignUpService.SignUpAsync(Email, Username, Password);
                Submitted?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                //IsLoadingProp.Set(false);
            }
        }
    }
}