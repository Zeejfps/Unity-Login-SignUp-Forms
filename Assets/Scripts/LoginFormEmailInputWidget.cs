using YADBF;

namespace Login
{
    internal sealed class LoginFormEmailInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();
        public LoginFormEmailInputWidget(ILoginManager loginManager)
        {
            TextProp = loginManager.EmailProp;
            IsInteractableProp.Bind(loginManager.IsLoadingProp, value => !value);
        }
    }
}