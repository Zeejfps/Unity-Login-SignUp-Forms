using System;
using Common.Controllers;
using Common.Widgets;
using Services;
using UnityEngine;
using Validation;
using YADBF;

namespace LoginForm
{
    internal sealed class LoginFormWidgetController : ILoginFormWidgetController
    {
        public string Email
        {
            get => EmailInputWidget.TextProp.Value;
            set => EmailInputWidget.TextProp.Value = value;
        }

        public string Password
        {
            get => PasswordInputWidget.TextProp.Value;
            set => PasswordInputWidget.TextProp.Value = value;
        }

        private bool m_IsLoading;
        public bool IsLoading
        {
            get => m_IsLoading;
            set
            {
                m_IsLoading = value;
                OnIsLoadingStateChanged();
            }
        }

        public bool IsRememberMeChecked
        {
            get => RememberMeToggleWidget.IsOnProp.Value;
            set => RememberMeToggleWidget.IsOnProp.Value = value;
        }
    
        public bool IsEmailValid { get; private set; }
        public bool IsPasswordValid { get; private set; }


        private bool CanSubmitForm => !IsLoading && IsEmailValid && IsPasswordValid;

        private ITextInputWidget EmailInputWidget => EmailFieldWidget.TextInputWidget;
        private ITextFieldWidget EmailFieldWidget => LoginFormWidget.EmailFieldWidget;
        private ITextInputWidget PasswordInputWidget => PasswordFieldWidget.TextInputWidget;
        private IPasswordFieldWidget PasswordFieldWidget => LoginFormWidget.PasswordFieldWidget;
        private IButtonWidget SubmitButtonWidget => LoginFormWidget.SubmitButtonWidget;
        private IToggleWidget RememberMeToggleWidget => LoginFormWidget.RememberMeToggleWidget;
    
        private ILoginService LoginService { get; }
        private IEmailValidator EmailValidator { get; }
        private ILoginFormWidget LoginFormWidget { get; }
    
        private IWidgetFocusController FocusController { get; }

        public LoginFormWidgetController(ILoginService loginService, IEmailValidator emailValidator, ILoginFormWidget loginFormWidget)
        {
            LoginService = loginService;
            EmailValidator = emailValidator;
            LoginFormWidget = loginFormWidget;
            SubmitButtonWidget.ActionProp.Set(SubmitForm);

            FocusController = new FocusController
            {
                CanCycle = true
            };
            FocusController.Add(EmailInputWidget);
            FocusController.Add(PasswordInputWidget);
            FocusController.Add(RememberMeToggleWidget);
            FocusController.Add(SubmitButtonWidget);
            FocusController.FocusFirstWidget();
        
            IsLoading = false;
            IsRememberMeChecked = true;
        
            LoginFormWidget.IsVisibleProp.ValueChanged += LoginFormWidget_IsVisibleProp_OnValueChanged;
            EmailInputWidget.TextProp.ValueChanged += EmailInputWidget_TextProp_OnValueChanged;
            PasswordInputWidget.TextProp.ValueChanged += PasswordInputWidget_TextProp_OnValueChanged;

            UpdateSubmitButtonInteractionState();
        }

        public bool ProcessInputEvent(InputEvent inputEvent)
        {
            if (LoginFormWidget.IsVisibleProp.IsFalse())
                return false;

            if (FocusController.ProcessInputEvent(inputEvent))
                return true;

            if (inputEvent == InputEvent.Submit && FocusController.FocusedWidget == PasswordInputWidget)
            {
                ValidateAndSubmitForm();
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            EmailInputWidget.TextProp.ValueChanged -= EmailInputWidget_TextProp_OnValueChanged;
            PasswordInputWidget.TextProp.ValueChanged -= PasswordInputWidget_TextProp_OnValueChanged;
        }

        private void LoginFormWidget_IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isVisible)
        {
            if (isVisible)
                FocusController.FocusFirstWidget();
        }

        private void EmailInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            ValidateEmail();
        }

        private void PasswordInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            ValidatePassword();
        }

        private void ValidateAndSubmitForm()
        {
            ValidateEmail();
            ValidatePassword();
        
            if (CanSubmitForm)
                SubmitForm();
        }
    
        private void ValidateEmail()
        {
            var isEmailValid = true;
            var validationResult = EmailValidator.Validate(Email);
            if (validationResult == EmailValidationStatus.Empty)
            {
                isEmailValid = false;
                EmailFieldWidget.ErrorTextProp.Set("Email is required");
            }
            else if (validationResult == EmailValidationStatus.Invalid)
            {
                isEmailValid = false;
                EmailFieldWidget.ErrorTextProp.Set("Invalid email");
            }
            else
            {
                EmailFieldWidget.ErrorTextProp.Set(string.Empty);
            }

            IsEmailValid = isEmailValid;
            UpdateSubmitButtonInteractionState();
        }

        private void ValidatePassword()
        {
            IsPasswordValid = !string.IsNullOrWhiteSpace(Password);
            if (!IsPasswordValid)
            {
                PasswordFieldWidget.ErrorTextProperty.Set("Password is required");
            }
            else
            {
                PasswordFieldWidget.ErrorTextProperty.Set(string.Empty);
            }
        
            UpdateSubmitButtonInteractionState();
        }

        private void OnIsLoadingStateChanged()
        {
            var isLoading = IsLoading;
            EmailInputWidget.IsInteractableProperty.Set(!isLoading);
            PasswordInputWidget.IsInteractableProperty.Set(!isLoading);
            RememberMeToggleWidget.IsInteractableProperty.Set(!isLoading);
            SubmitButtonWidget.IsLoadingProp.Set(isLoading);
            UpdateSubmitButtonInteractionState();
        }

        private void UpdateSubmitButtonInteractionState()
        {
            var canSubmitForm = CanSubmitForm;
            SubmitButtonWidget.IsInteractableProperty.Set(canSubmitForm);
        }

        private async void SubmitForm()
        {
            try
            {
                var email = Email;
                var password = Password;

                IsLoading = true;
                await LoginService.LoginAsync(email, password);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}