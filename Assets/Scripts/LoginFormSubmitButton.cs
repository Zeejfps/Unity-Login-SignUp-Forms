using System;
using YADBF;

namespace Login
{
    internal sealed class LoginFormSubmitButton : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();
        public ObservableProperty<bool> IsLoadingProp { get; } 

        private ILoginFormWidgetController LoginFormWidgetController { get; }
        
        public LoginFormSubmitButton(ILoginFormWidgetController loginFormWidgetController)
        {
            LoginFormWidgetController = loginFormWidgetController;
            // IsLoadingProp = loginFormWidgetController.IsLoadingProp;
            // LoginFormWidgetController.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            // LoginFormWidgetController.SubmitActionProp.ValueChanged += LoginActionProp_OnValueChanged;
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
            // var loginAction = LoginFormWidgetController.SubmitActionProp.Value;
            // var isLoading = LoginFormWidgetController.IsLoadingProp.Value;
            //
            // ActionProp.Set(loginAction);
            // IsInteractableProp.Set(loginAction != null && !isLoading);
        }
    }
}