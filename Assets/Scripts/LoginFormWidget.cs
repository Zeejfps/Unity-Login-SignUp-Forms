using System;
using UnityEngine;
using YADBF;

namespace Login
{
    internal sealed class LoginFormWidget : ILoginFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ITextInputWidget EmailInputWidget { get; }
        public IPasswordFieldWidget PasswordInputWidget { get; }
        public IButtonWidget LoginButtonWidget { get; }

        private ILoginManager LoginManager { get; }
        
        public LoginFormWidget(ILoginManager loginManager)
        {
            LoginManager = loginManager;
            EmailInputWidget = new LoginFormEmailInputWidget(loginManager);
            PasswordInputWidget = new PasswordFieldWidget(new LoginFormPasswordInputWidget(loginManager));
            LoginButtonWidget = new LoginFormLoginButton(loginManager);
        }

        public void Dispose()
        {
        }
    }
}