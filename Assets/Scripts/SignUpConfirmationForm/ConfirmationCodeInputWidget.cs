using YADBF;

namespace SignUpConfirmationForm
{
    public sealed class ConfirmationCodeInputWidget : BaseTextInputWidget
    {
        private ISignUpConfirmationPopupWidgetController SignUpConfirmationForm { get; }
    
        public ConfirmationCodeInputWidget(ISignUpConfirmationPopupWidgetController signUpConfirmationForm)
        {
            SignUpConfirmationForm = signUpConfirmationForm;
            // SignUpConfirmationForm.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;
            //
            // TextProp = signUpConfirmationForm.ConfirmationCodeTextProp;
        
            UpdateState();
        }

        private void IsLoadingProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            // var isLoading = SignUpConfirmationForm.IsLoadingProp.Value;
            // IsInteractableProperty.Set(!isLoading);
        }
    }
}