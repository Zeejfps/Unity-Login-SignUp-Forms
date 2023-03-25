using YADBF;

namespace Login
{
    internal sealed class SignUpFormPasswordInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new(true);

        private ISignUpForm SignUpForm { get; }

        public SignUpFormPasswordInputWidget(ISignUpForm signUpForm)
        {
            SignUpForm = signUpForm;
            TextProp = signUpForm.PasswordProp;
            IsInteractableProp.Bind(signUpForm.IsLoadingProp, value => !value);
            signUpForm.Submitted += SignUpFlow_OnCompleted;
        }

        private void SignUpFlow_OnCompleted()
        {
            TextProp.Set(string.Empty);
        }
    }
}