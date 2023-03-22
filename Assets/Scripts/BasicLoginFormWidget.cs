using System;
using UnityEngine;
using YADBF;

namespace Login
{
    internal sealed class BasicLoginFormWidget : ILoginFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ObservableProperty<ITextInputWidget> EmailInputWidgetProp { get; } = new(new BasicTextInputWidget());
        public ObservableProperty<ITextInputWidget> PasswordInputWidgetProp { get; } = new(new BasicTextInputWidget());
        public ObservableProperty<Action> LoginActionProp { get; } = new();

        private ILoginService LoginService { get; }
        private IPopupService PopupService { get; }

        private ITextInputWidget EmailInputWidget => EmailInputWidgetProp.Value;
        private ITextInputWidget PasswordInputWidget => PasswordInputWidgetProp.Value;
        private string Email => EmailInputWidgetProp.Value.TextProp.Value;
        private string Password => PasswordInputWidgetProp.Value.TextProp.Value;
    
        public BasicLoginFormWidget(ILoginService loginService, IPopupService popupService)
        {
            LoginService = loginService;
            PopupService = popupService;
            
            EmailInputWidget.TextProp.ValueChanged += EmailProp_OnValueChanged;
            PasswordInputWidget.TextProp.ValueChanged += PasswordProp_OnValueChanged;
        }

        public void Dispose()
        {
        }

        private void EmailProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateLoginAction();
        }

        private void PasswordProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateLoginAction();
        }

        private void UpdateLoginAction()
        {
            var email = Email;
            var password = Password;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                LoginActionProp.Set(null);
            else
                LoginActionProp.Set(HandleLoginAction);
        }

        private async void HandleLoginAction()
        {
            try
            {
                LoginActionProp.Set(null);
                EmailInputWidget.IsInteractableProp.Set(false);
                PasswordInputWidget.IsInteractableProp.Set(false);

                var email = Email;
                var password = Password;

                var error = await LoginService.LoginAsync(email, password);
                if (error == LoginError.InvalidCredentials) 
                    await PopupService.ShowInfoPopupAsync("Invalid Credentials", "Email and/or Password was incorrect");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                EmailInputWidget.IsInteractableProp.Set(true);
                PasswordInputWidget.IsInteractableProp.Set(true);
                UpdateLoginAction();
            }
        }
    }
}