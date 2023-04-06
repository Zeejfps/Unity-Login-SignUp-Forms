using System;
using Login;
using YADBF;

namespace SignUpConfirmationForm
{
    public sealed class CancelConfirmSignUpButtonWidget : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<Action> ActionProp { get; } = new();
        public ObservableProperty<bool> IsLoadingProp { get; } = new();

        private ISignUpConfirmationFormWidgetController SignUpConfirmationForm { get; }
        private IPopupWidget PopupWidget { get; }
    
        public CancelConfirmSignUpButtonWidget(ISignUpConfirmationFormWidgetController signUpConfirmationForm, IPopupWidget popupWidget)
        {
            SignUpConfirmationForm = signUpConfirmationForm;
            PopupWidget = popupWidget;
        
            SignUpConfirmationForm.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            ActionProp.Set(Cancel);

            UpdateState();
        }

        private void Cancel()
        {
            PopupWidget.IsVisibleProp.Set(false);
        }

        private void IsLoadingProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            var isLoading = SignUpConfirmationForm.IsLoadingProp.Value;
            IsInteractableProperty.Set(!isLoading);
        }

        public ObservableProperty<bool> IsFocusedProperty { get; } = new();
        public ObservableProperty<bool> IsInteractableProperty { get; } = new();
    }
}