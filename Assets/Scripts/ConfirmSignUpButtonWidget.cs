using System;
using YADBF;

namespace Login
{
    public sealed class ConfirmSignUpButtonWidget : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();
        public ObservableProperty<bool> IsLoadingProp { get; }

        private ISignUpConfirmationFlow SignUpConfirmationFlow { get; }
        
        public ConfirmSignUpButtonWidget(ISignUpConfirmationFlow signUpConfirmationFlow)
        {
            SignUpConfirmationFlow = signUpConfirmationFlow;
            IsLoadingProp = signUpConfirmationFlow.IsLoadingProp;
            
            SignUpConfirmationFlow.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            SignUpConfirmationFlow.ConfirmationCodeTextProp.ValueChanged += ConfirmationCodeText_PropOnValueChanged;
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
            var confirmAction = SignUpConfirmationFlow.ConfirmActionProp.Value;
            var isLoading = SignUpConfirmationFlow.IsLoadingProp.Value;
            IsInteractableProp.Set(confirmAction != null && !isLoading);
            ActionProp.Set(confirmAction);
        }
    }
}