using System;
using YADBF;

namespace Login
{
    internal sealed class SignUpFormSignUpButton : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();
        public ObservableProperty<bool> IsLoadingProp { get; }

        private ISignUpFlow SignUpFlow { get; }
        
        public SignUpFormSignUpButton(ISignUpFlow signUpFlow)
        {
            SignUpFlow = signUpFlow;
            IsLoadingProp = signUpFlow.IsLoadingProp;
            
            signUpFlow.SignUpActionProp.ValueChanged += SignUpActionProp_OnValueChanged;
            signUpFlow.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            UpdateState();
        }

        private void IsLoadingProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
        {
            UpdateState();
        }

        private void SignUpActionProp_OnValueChanged(ObservableProperty<Action> property, Action prevvalue, Action currvalue)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            var signUpAction = SignUpFlow.SignUpActionProp.Value;
            var isLoading = SignUpFlow.IsLoadingProp.Value;
            
            ActionProp.Set(signUpAction);
            IsInteractableProp.Set(signUpAction != null && !isLoading);
        }
    }
}