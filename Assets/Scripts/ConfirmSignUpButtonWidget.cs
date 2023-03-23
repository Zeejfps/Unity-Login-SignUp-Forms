using System;
using YADBF;

namespace Login
{
    public sealed class ConfirmSignUpButtonWidget : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractable { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();

        private ISignUpConfirmationManager SignUpConfirmationManager { get; }
        
        public ConfirmSignUpButtonWidget(ISignUpConfirmationManager signUpConfirmationManager)
        {
            SignUpConfirmationManager = signUpConfirmationManager;
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