using System;
using YADBF;

namespace Login
{
    internal sealed class LoginFormLoginButton : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractable { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();

        private ILoginManager LoginManager { get; }
        
        public LoginFormLoginButton(ILoginManager loginManager)
        {
            LoginManager = loginManager;
        }
        
        private void UpdateState()
        {
            var loginAction = LoginManager.LoginActionProp.Value;
            var isLoading = LoginManager.IsLoadingProp.Value;
            
            ActionProp.Set(loginAction);
            IsInteractable.Set(loginAction != null && !isLoading);
        }
    }
}