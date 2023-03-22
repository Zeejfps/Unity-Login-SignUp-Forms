using YADBF;

namespace Login
{
    internal sealed class SignUpFormPasswordInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new(true);
        
        public SignUpFormPasswordInputWidget(ISignUpManager signUpManager)
        {
            TextProp = signUpManager.PasswordProp;
            IsInteractableProp.Bind(signUpManager.IsLoadingProp, value => !value);
        }
    }
}