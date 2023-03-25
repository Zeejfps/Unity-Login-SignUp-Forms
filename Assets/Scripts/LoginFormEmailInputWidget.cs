using YADBF;

namespace Login
{
    internal sealed class LoginFormEmailInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();
        public LoginFormEmailInputWidget(ILoginForm loginForm)
        {
            TextProp = loginForm.EmailProp;
            IsInteractableProp.Bind(loginForm.IsLoadingProp, value => !value);
        }
    }
}