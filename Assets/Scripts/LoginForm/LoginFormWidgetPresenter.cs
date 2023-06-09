using System;
using Common.Controllers;
using Common.Widgets;
using InfoPopup;
using Login;
using Services;
using UnityEngine;
using Validators;
using YADBF;

namespace LoginForm
{
    internal sealed class LoginFormWidgetPresenter : ILoginFormWidgetPresenter
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
    
        private IPopupManager PopupService { get; }
        private IFocusGroup FocusGroup { get; }
        private IInfoPopupWidgetPresenter InfoPopupWidgetPresenter { get; }

        public LoginFormWidgetPresenter(
            IPopupManager popupService,
            ILoginService loginService, 
            IEmailValidator emailValidator,
            ILoginFormWidget loginFormWidget)
        {
            PopupService = popupService;
            LoginService = loginService;
            EmailValidator = emailValidator;
            LoginFormWidget = loginFormWidget;
            SubmitButtonWidget.ActionProp.Set(SubmitForm);

            InfoPopupWidgetPresenter = new InfoPopupWidgetPresenter(PopupService);
            FocusGroup = new FocusGroup
            {
                CanCycle = true
            };
            FocusGroup.Add(EmailInputWidget);
            FocusGroup.Add(PasswordInputWidget);
            FocusGroup.Add(RememberMeToggleWidget);
            FocusGroup.Add(SubmitButtonWidget);
            FocusGroup.FocusFirstWidget();
        
            IsLoading = false;
            IsRememberMeChecked = true;
        
            LoginFormWidget.IsVisibleProperty.ValueChanged += LoginFormWidget_IsVisibleProp_OnValueChanged;
            EmailInputWidget.TextProp.ValueChanged += EmailInputWidget_TextProp_OnValueChanged;
            PasswordInputWidget.TextProp.ValueChanged += PasswordInputWidget_TextProp_OnValueChanged;

            UpdateSubmitButtonInteractionState();
        }

        public bool ProcessInputEvent(InputEvent inputEvent)
        {
            if (LoginFormWidget.IsVisibleProperty.IsFalse())
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
            
            if (inputEvent == InputEvent.Submit && FocusGroup.FocusedWidget == PasswordInputWidget)
            {
                ValidateAndSubmitForm();
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            LoginFormWidget.IsVisibleProperty.ValueChanged -= LoginFormWidget_IsVisibleProp_OnValueChanged;
            EmailInputWidget.TextProp.ValueChanged -= EmailInputWidget_TextProp_OnValueChanged;
            PasswordInputWidget.TextProp.ValueChanged -= PasswordInputWidget_TextProp_OnValueChanged;
        }

        private void LoginFormWidget_IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isVisible)
        {
            if (isVisible)
                FocusGroup.FocusFirstWidget();
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
                var result = await LoginService.LoginAsync(email, password);
                if (result != LoginResult.Success)
                    await InfoPopupWidgetPresenter.ShowAndWaitUntilClosed("Login Error", "Invalid Email and / or Password");
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