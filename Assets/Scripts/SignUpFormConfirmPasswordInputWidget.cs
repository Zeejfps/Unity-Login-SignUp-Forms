using YADBF;

namespace Login
{
    internal class SignUpFormConfirmPasswordInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new(true);

        public SignUpFormConfirmPasswordInputWidget(ISignUpManager signUpManager)
        {
            TextProp = signUpManager.ConfirmPasswordProp;
            IsInteractableProp.Bind(signUpManager.IsLoadingProp, value => !value);   
        }
    }
}