using YADBF;

namespace Login
{
    internal sealed class LoginFormPasswordInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new(true);

        public LoginFormPasswordInputWidget(ILoginForm loginForm)
        {
            TextProp = loginForm.PasswordProp;
            IsInteractableProp.Bind(loginForm.IsLoadingProp, value => !value);
        }
    }
}