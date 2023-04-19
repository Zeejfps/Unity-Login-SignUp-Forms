using System;
using Common.Controllers;
using Common.Widgets;
using Services;
using Tests;
using UnityEngine;
using Validators;
using YADBF;

namespace SignUpForm
{
    public sealed class SignUpFormWidgetPresenter : ISignUpFormWidgetPresenter
    {
        public event Action FormSubmitted;
        public event Action EmailChanged;
        public event Action UsernameChanged;
        public event Action PasswordChanged;
        public event Action ConfirmPasswordChanged;
    
        public string Email => EmailInputWidget.TextProp.Value;
        public string Username => UsernameInputWidget.TextProp.Value;

        public string Password
        {
            get => PasswordInputWidget.TextProp.Value;
            set => PasswordInputWidget.TextProp.Value = value;
        }
    
        public string ConfirmPassword
        {
            get => ConfirmPasswordInputWidget.TextProp.Value;

            set => ConfirmPasswordInputWidget.TextProp.Value = value;
        }

        private bool m_IsLoading;
        public bool IsLoading
        {
            get => m_IsLoading;
            set
            {
                m_IsLoading = value;
                OnLoadingStateChanged();
            }
        }

        private ITextInputWidget EmailInputWidget => EmailFieldWidget.TextInputWidget;
        private ITextFieldWidget EmailFieldWidget => SignUpFormWidget.EmailFieldWidget;
        private ITextInputWidget UsernameInputWidget => UsernameFieldWidget.TextInputWidget;
        private ITextFieldWidget UsernameFieldWidget => SignUpFormWidget.UsernameFieldWidget;
        private ITextInputWidget PasswordInputWidget => PasswordFieldWidget.TextInputWidget;
        private IPasswordFieldWidget PasswordFieldWidget => SignUpFormWidget.PasswordFieldWidget;
        private ITextInputWidget ConfirmPasswordInputWidget => ConfirmPasswordFieldWidget.TextInputWidget;
        private IPasswordFieldWidget ConfirmPasswordFieldWidget => SignUpFormWidget.ConfirmPasswordFieldWidget;
        private IButtonWidget SubmitButtonWidget => SignUpFormWidget.SubmitButtonWidget;
        
        private ISignUpFormWidget SignUpFormWidget { get; }
        private IEmailValidator EmailValidator { get; }
        private IPasswordValidator[] PasswordValidators { get; }
        private ISignUpService SignUpService { get; }

        private bool IsEmailValid { get; set; }
        private bool IsUsernameValid { get; set; }
        private bool IsPasswordValid { get; set; }
        private bool IsConfirmPasswordValid { get; set; }

        private IStateMachine StateMachine { get; }
        private IFocusGroup FocusGroup { get; }

        public SignUpFormWidgetPresenter(
            ISignUpService signUpService, 
            IEmailValidator emailValidator, 
            IPasswordValidator[] passwordValidators,
            ISignUpFormWidget signUpFormWidget)
        {
            SignUpService = signUpService;
            SignUpFormWidget = signUpFormWidget;
            
            EmailValidator = emailValidator;
            PasswordValidators = passwordValidators;
            StateMachine = new SimpleStateMachine();

            SubmitButtonWidget.ActionProp.Set(SubmitForm);
            SignUpFormWidget.IsVisibleProperty.ValueChanged += SignUpFormWidget_IsVisibleProp_OnValueChanged;
            EmailInputWidget.TextProp.ValueChanged += EmailInputWidget_TextProp_OnValueChanged;
            UsernameInputWidget.TextProp.ValueChanged += UsernameInputWidget_TextProp_OnValueChanged;
            PasswordInputWidget.TextProp.ValueChanged += PasswordInputWidget_TextProp_OnValueChanged;
            ConfirmPasswordInputWidget.TextProp.ValueChanged += ConfirmPasswordInputWidget_TextProp_OnValueChanged;

            foreach (var passwordRequirement in PasswordValidators)
            {
                var widget = new PasswordRequirementWidget();
                widget.Description.Set(passwordRequirement.Description);
                SignUpFormWidget.PasswordRequirementsListWidget.Add(widget);
            }

            StateMachine.State = new SignUpFormWidgetPresenterDefaultState(this);

            FocusGroup = new FocusGroup
            {
                CanCycle = true
            };
            FocusGroup.Add(EmailInputWidget);
            FocusGroup.Add(UsernameInputWidget);
            FocusGroup.Add(PasswordInputWidget);
            FocusGroup.Add(ConfirmPasswordInputWidget);
            FocusGroup.Add(SubmitButtonWidget);
        }

        public void Dispose()
        {
            FocusGroup.Dispose();
            SignUpFormWidget.IsVisibleProperty.ValueChanged -= SignUpFormWidget_IsVisibleProp_OnValueChanged;
            EmailInputWidget.TextProp.ValueChanged -= EmailInputWidget_TextProp_OnValueChanged;
            UsernameInputWidget.TextProp.ValueChanged -= UsernameInputWidget_TextProp_OnValueChanged;
            PasswordInputWidget.TextProp.ValueChanged -= PasswordInputWidget_TextProp_OnValueChanged;
            ConfirmPasswordInputWidget.TextProp.ValueChanged -= ConfirmPasswordInputWidget_TextProp_OnValueChanged;
            StateMachine.State = null;
        }

        public bool ProcessInputEvent(InputEvent inputEvent)
        {
            if (SignUpFormWidget.IsVisibleProperty.IsFalse())
                return false;

            if (inputEvent == InputEvent.FocusNext)
            {
                FocusGroup.FocusNext();
                return true;
            }

            if (inputEvent == InputEvent.FocusPrevious)
            {
                FocusGroup.FocusPrev();
                return true;
            }

            return false;
        }

        private void SignUpFormWidget_IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool wasFocused, bool isFocused)
        {
            if (isFocused)
                FocusGroup.FocusFirstWidget();
        }

        private void EmailInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            EmailChanged?.Invoke();
        }

        private void UsernameInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UsernameChanged?.Invoke();
        }
        
        private void PasswordInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            PasswordChanged?.Invoke();
        }
        
        private void ConfirmPasswordInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            ConfirmPasswordChanged?.Invoke();
        }

        public void ValidateEmail()
        {
            var email = Email;
            var isEmailValid = true;
            if (string.IsNullOrWhiteSpace(email))
            {
                EmailFieldWidget.ErrorTextProp.Set("Email is required");
                isEmailValid = false;
            }
            else if (EmailValidator.Validate(email) == EmailValidationStatus.Invalid)
            {
                EmailFieldWidget.ErrorTextProp.Set("Email is invalid");
                isEmailValid = false;
            }
            else
            {
                EmailFieldWidget.ErrorTextProp.Set(string.Empty);
            }

            IsEmailValid = isEmailValid;
            UpdateSubmitButtonState();
        }

        public void ValidateUsername()
        {
            var username = Username;
            var isUsernameValid = true;

            if (string.IsNullOrWhiteSpace(username))
            {
                UsernameFieldWidget.ErrorTextProp.Set("Username is required");
                isUsernameValid = false;
            }
            else
            {
                UsernameFieldWidget.ErrorTextProp.Set(string.Empty);
            }

            IsUsernameValid = isUsernameValid;
            UpdateSubmitButtonState();
        }

        public void ValidatePassword()
        {
            var password = Password;
            var isPasswordValid = true;
        
            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordFieldWidget.ErrorTextProperty.Set("Password is required");
                isPasswordValid = false;
            }
            else if (!CheckAllPasswordRequirements(password))
            {
                PasswordFieldWidget.ErrorTextProperty.Set("Not all requirements met");
                isPasswordValid = false;
            }
            else
            {
                PasswordFieldWidget.ErrorTextProperty.Set(string.Empty);
            }

            IsPasswordValid = isPasswordValid;
            UpdateSubmitButtonState();
        }

        private bool CheckAllPasswordRequirements(string password)
        {
            var allRequirementsValid = true;
            var passwordRequirements = PasswordValidators;
            for (var i = 0; i < passwordRequirements.Length; i++)
            {
                var requirement = passwordRequirements[i];
                var requirementWidget = SignUpFormWidget.PasswordRequirementsListWidget.Items[i];
                var isRequirementValid = requirement.Validate(password);
                requirementWidget.IsMet.Set(isRequirementValid);
                allRequirementsValid &= isRequirementValid;
            }

            return allRequirementsValid;
        }

        public void ValidateConfirmPassword()
        {
            var password = Password;
            var confirmPassword = ConfirmPassword;
            var isConfirmPasswordValid = true;

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                ConfirmPasswordFieldWidget.ErrorTextProperty.Set("Please confirm password");
                isConfirmPasswordValid = false;
            }
            else if (confirmPassword != password)
            {
                ConfirmPasswordFieldWidget.ErrorTextProperty.Set("Passwords do not match");
                isConfirmPasswordValid = false;
            }
            else
            {
                ConfirmPasswordFieldWidget.ErrorTextProperty.Set(string.Empty);
            }

            IsConfirmPasswordValid = isConfirmPasswordValid;
            UpdateSubmitButtonState();
        }

        private void OnLoadingStateChanged()
        {
            var isLoading = IsLoading;
            EmailInputWidget.IsInteractableProperty.Set(!isLoading);
            UsernameInputWidget.IsInteractableProperty.Set(!isLoading);
            PasswordInputWidget.IsInteractableProperty.Set(!isLoading);
            ConfirmPasswordInputWidget.IsInteractableProperty.Set(!isLoading);
            SubmitButtonWidget.IsLoadingProp.Set(isLoading);
            UpdateSubmitButtonState();
        }

        private void UpdateSubmitButtonState()
        {
            SubmitButtonWidget.IsInteractableProperty.Set(IsEmailValid &&
                                                          IsUsernameValid &&
                                                          IsPasswordValid &&
                                                          IsConfirmPasswordValid &&
                                                          !IsLoading);
        }

        private async void SubmitForm()
        {
            try
            {
                StateMachine.State = new SignUpFormWidgetPresenterSubmittingFormState(this);
                var success = await SignUpService.SignUpAsync(Email, Username, Password);
                if (success) FormSubmitted?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                StateMachine.State = new SignUpFormWidgetPresenterDefaultState(this);
            }
        }
    }
}