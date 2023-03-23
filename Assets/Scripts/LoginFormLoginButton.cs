using System;
using YADBF;

namespace Login
{
    internal sealed class LoginFormLoginButton : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractable { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();

        private ILoginFlow LoginFlow { get; }
        
        public LoginFormLoginButton(ILoginFlow loginFlow)
        {
            LoginFlow = loginFlow;
            LoginFlow.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            LoginFlow.LoginActionProp.ValueChanged += LoginActionProp_OnValueChanged;
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
            var loginAction = LoginFlow.LoginActionProp.Value;
            var isLoading = LoginFlow.IsLoadingProp.Value;
            
            ActionProp.Set(loginAction);
            IsInteractable.Set(loginAction != null && !isLoading);
        }
    }
}