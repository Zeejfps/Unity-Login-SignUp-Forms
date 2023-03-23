using YADBF;

namespace Login
{
    internal sealed class LoginFormEmailInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();
        public LoginFormEmailInputWidget(ILoginFlow loginFlow)
        {
            TextProp = loginFlow.EmailProp;
            IsInteractableProp.Bind(loginFlow.IsLoadingProp, value => !value);
        }
    }
}