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

        private ISignUpConfirmationForm SignUpConfirmationForm { get; }
        
        public ConfirmSignUpButtonWidget(ISignUpConfirmationForm signUpConfirmationForm)
        {
            SignUpConfirmationForm = signUpConfirmationForm;
            IsLoadingProp = signUpConfirmationForm.IsLoadingProp;
            
            SignUpConfirmationForm.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            SignUpConfirmationForm.ConfirmationCodeTextProp.ValueChanged += ConfirmationCodeText_PropOnValueChanged;
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
            var confirmAction = SignUpConfirmationForm.ConfirmActionProp.Value;
            var isLoading = SignUpConfirmationForm.IsLoadingProp.Value;
            IsInteractableProp.Set(confirmAction != null && !isLoading);
            ActionProp.Set(confirmAction);
        }

        public ObservableProperty<bool> IsFocusedProperty { get; } = new();
        public ObservableProperty<bool> CanBeFocusedProperty { get; } = new();
    }
}