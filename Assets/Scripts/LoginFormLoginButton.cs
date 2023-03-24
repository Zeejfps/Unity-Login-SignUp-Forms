using System;
using YADBF;

namespace Login
{
    internal sealed class LoginFormLoginButton : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();
        public ObservableProperty<bool> IsLoadingProp { get; } 

        private ILoginFlow LoginFlow { get; }
        
        public LoginFormLoginButton(ILoginFlow loginFlow)
        {
            LoginFlow = loginFlow;
            IsLoadingProp = loginFlow.IsLoadingProp;
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
            IsInteractableProp.Set(loginAction != null && !isLoading);
        }
    }
}