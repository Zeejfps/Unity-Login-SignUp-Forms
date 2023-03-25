using YADBF;

namespace Login
{
    internal sealed class SignUpFormEmailInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();
        
        public SignUpFormEmailInputWidget(ISignUpForm signUpManager)
        {
            TextProp = signUpManager.EmailProp;
            IsInteractableProp.Bind(signUpManager.IsLoadingProp, value => !value);
        }

        public void Dispose()
        {
            IsInteractableProp.Unbind();
        }
    }
}