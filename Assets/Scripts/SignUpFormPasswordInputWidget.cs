using YADBF;

namespace Login
{
    internal sealed class SignUpFormPasswordInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new(true);

        private ISignUpFlow SignUpFlow { get; }

        public SignUpFormPasswordInputWidget(ISignUpFlow signUpFlow)
        {
            SignUpFlow = signUpFlow;
            TextProp = signUpFlow.PasswordProp;
            IsInteractableProp.Bind(signUpFlow.IsLoadingProp, value => !value);
            signUpFlow.Completed += SignUpFlow_OnCompleted;
        }

        private void SignUpFlow_OnCompleted()
        {
            TextProp.Set(string.Empty);
        }
    }
}