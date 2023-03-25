using YADBF;

namespace Login
{
    internal sealed class SignUpFormUsernameInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();

        public SignUpFormUsernameInputWidget(ISignUpForm signUpForm)
        {
            TextProp = signUpForm.UsernameProp;
            IsInteractableProp.Bind(signUpForm.IsLoadingProp, value => !value);
        }
    }
}