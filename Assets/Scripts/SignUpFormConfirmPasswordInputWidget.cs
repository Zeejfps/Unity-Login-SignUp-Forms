using YADBF;

namespace Login
{
    internal class SignUpFormConfirmPasswordInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new(true);
        
        public SignUpFormConfirmPasswordInputWidget(ISignUpFlow signUpFlow)
        {
            TextProp = signUpFlow.ConfirmPasswordProp;
            IsInteractableProp.Bind(signUpFlow.IsLoadingProp, value => !value);   
            signUpFlow.Completed += SignUpFlow_OnCompleted;
        }

        private void SignUpFlow_OnCompleted()
        {
            TextProp.Set(string.Empty);
        }
    }
}