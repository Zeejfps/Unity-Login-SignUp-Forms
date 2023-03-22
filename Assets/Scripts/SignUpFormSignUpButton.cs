using System;
using YADBF;

namespace Login
{
    internal sealed class SignUpFormSignUpButton : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractable { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();

        private ISignUpManager SignUpManager { get; }
        
        public SignUpFormSignUpButton(ISignUpManager signUpManager)
        {
            SignUpManager = signUpManager;
            signUpManager.SignUpActionProp.ValueChanged += SignUpActionProp_OnValueChanged;
            UpdateState();
        }

        private void SignUpActionProp_OnValueChanged(ObservableProperty<Action> property, Action prevvalue, Action currvalue)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            var signUpAction = SignUpManager.SignUpActionProp.Value;
            ActionProp.Set(signUpAction);
            IsInteractable.Set(signUpAction != null);
        }
    }
}