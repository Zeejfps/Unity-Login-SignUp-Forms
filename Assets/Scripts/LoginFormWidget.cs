using YADBF;

namespace Login
{
    internal sealed class LoginFormWidget : ILoginFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ITextInputWidget EmailInputWidget { get; }
        public IPasswordFieldWidget PasswordInputWidget { get; }
        public IButtonWidget LoginButtonWidget { get; }

        private ILoginFlow LoginFlow { get; }
        
        public LoginFormWidget(ILoginFlow loginFlow)
        {
            LoginFlow = loginFlow;
            EmailInputWidget = new LoginFormEmailInputWidget(loginFlow);
            PasswordInputWidget = new PasswordFieldWidget(new LoginFormPasswordInputWidget(loginFlow));
            LoginButtonWidget = new LoginFormLoginButton(loginFlow);
        }

        public void Dispose()
        {
        }
    }
}