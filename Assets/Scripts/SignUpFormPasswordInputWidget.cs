using YADBF;

namespace Login
{
    internal sealed class SignUpFormPasswordInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new(true);
        
        public SignUpFormPasswordInputWidget(ISignUpForm signUpForm)
        {
            TextProp = signUpForm.PasswordProp;
            IsInteractableProp.Bind(signUpForm.IsLoadingProp, value => !value);
        }
    }
}