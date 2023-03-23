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
            LoginManager.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            LoginManager.LoginActionProp.ValueChanged += LoginActionProp_OnValueChanged;
            UpdateState();
        }

        private void LoginActionProp_OnValueChanged(ObservableProperty<Action> property, Action prevvalue, Action currvalue)
        {
            UpdateState();
        }

        private void IsLoadingProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
        {
            UpdateState();
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