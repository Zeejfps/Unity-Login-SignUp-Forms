using YADBF;

namespace Login
{
    internal sealed class LoginFormPasswordInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; }
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new(true);

        public LoginFormPasswordInputWidget(ILoginFlow loginFlow)
        {
            TextProp = loginFlow.PasswordProp;
            IsInteractableProp.Bind(loginFlow.IsLoadingProp, value => !value);
        }
    }
}