using System;
using UnityEngine;
using YADBF;

namespace Login
{
    internal sealed class BasicLoginFormWidget : ILoginFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ITextInputWidget EmailInputWidget { get; } = new BasicTextInputWidget();
        public IPasswordInputWidget PasswordInputWidget { get; } =new BasicPasswordInputWidget();
        public ObservableProperty<Action> LoginActionProp { get; } = new();

        private ILoginService LoginService { get; }
        
        private string Email => EmailInputWidget.TextProp.Value;
        private string Password => PasswordInputWidget.TextInputWidget.TextProp.Value;
    
        public BasicLoginFormWidget(ILoginService loginService)
        {
            LoginService = loginService;
            
            EmailInputWidget.TextProp.ValueChanged += EmailProp_OnValueChanged;
            PasswordInputWidget.TextInputWidget.TextProp.ValueChanged += PasswordProp_OnValueChanged;
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
                PasswordInputWidget.TextInputWidget.IsInteractableProp.Set(false);

                var email = Email;
                var password = Password;

                await LoginService.LoginAsync(email, password);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                EmailInputWidget.IsInteractableProp.Set(true);
                PasswordInputWidget.TextInputWidget.IsInteractableProp.Set(true);
                UpdateLoginAction();
            }
        }
    }
}