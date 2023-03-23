using System;
using YADBF;

namespace Login
{
    public sealed class ConfirmSignUpButtonWidget : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractable { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();

        private ISignUpConfirmation SignUpConfirmationManager { get; }
        
        public ConfirmSignUpButtonWidget(ISignUpConfirmation signUpConfirmationManager)
        {
            SignUpConfirmationManager = signUpConfirmationManager;
            SignUpConfirmationManager.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            SignUpConfirmationManager.ConfirmationCodeTextProp.ValueChanged += ConfirmationCodeText_PropOnValueChanged;
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
            var confirmAction = SignUpConfirmationManager.ConfirmActionProp.Value;
            var isLoading = SignUpConfirmationManager.IsLoadingProp.Value;
            IsInteractable.Set(confirmAction != null && !isLoading);
            ActionProp.Set(confirmAction);
        }
    }
}