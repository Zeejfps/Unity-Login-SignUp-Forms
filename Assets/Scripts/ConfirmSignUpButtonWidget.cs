using System;
using YADBF;

namespace Login
{
    public sealed class ConfirmSignUpButtonWidget : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractable { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();

        private ISignUpConfirmationFlow SignUpConfirmationFlowManager { get; }
        
        public ConfirmSignUpButtonWidget(ISignUpConfirmationFlow signUpConfirmationFlowManager)
        {
            SignUpConfirmationFlowManager = signUpConfirmationFlowManager;
            SignUpConfirmationFlowManager.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            SignUpConfirmationFlowManager.ConfirmationCodeTextProp.ValueChanged += ConfirmationCodeText_PropOnValueChanged;
            UpdateState();
        }

        private void IsLoadingProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
        {
            UpdateState();
        }

        private void ConfirmationCodeText_PropOnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            var confirmAction = SignUpConfirmationFlowManager.ConfirmActionProp.Value;
            var isLoading = SignUpConfirmationFlowManager.IsLoadingProp.Value;
            IsInteractable.Set(confirmAction != null && !isLoading);
            ActionProp.Set(confirmAction);
        }
    }
}