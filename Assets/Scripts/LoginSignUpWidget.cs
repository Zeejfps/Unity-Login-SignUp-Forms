using Login;
using YADBF;

public sealed class LoginSignUpWidget : ILoginSignUpWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITabWidget LoginFormTabWidget { get; }
    public ITabWidget SignUpFormTabWidget { get; }
    public ILoginFormWidget LoginFormWidget { get; }
    public ISignUpFormWidget SignUpFormWidget { get; }

    public LoginSignUpWidget(ILoginFlow loginFlow, ISignUpFlow signUpFlow)
    {
        LoginFormWidget = new LoginFormWidget(
            new LoginFormEmailInputWidget(loginFlow),
            new PasswordFieldWidget(new LoginFormPasswordInputWidget(loginFlow)),
            new LoginFormLoginButton(loginFlow));
        
        LoginFormTabWidget = new LoginFormTabWidget(signUpFlow, LoginFormWidget);
        
        SignUpFormWidget = new SignUpFormWidget(
            new SignUpFormEmailInputWidget(signUpFlow),
            new PasswordFieldWidget(new SignUpFormPasswordInputWidget(signUpFlow)),
            new PasswordFieldWidget(new SignUpFormConfirmPasswordInputWidget(signUpFlow)),
            new SignUpFormSignUpButton(signUpFlow));

        SignUpFormTabWidget = new SignUpFormTabWidget(loginFlow, SignUpFormWidget);

        var tabGroup = new TabGroup();
        tabGroup.AddTab(LoginFormTabWidget);
        tabGroup.AddTab(SignUpFormTabWidget);
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
        signUpFlow.Completed += SignUpFlow_OnCompleted; 
    }

    private void SignUpFlow_OnCompleted()
    {
        var signUpFormWidget = SignUpFormWidget;
        var loginFormWidget = LoginFormWidget;
        
        var email = signUpFormWidget.EmailInputWidget.TextProp.Value;
        var password = signUpFormWidget.PasswordFieldWidget.TextInputWidget.TextProp.Value;

        loginFormWidget.EmailInputWidget.TextProp.Set(email);
        loginFormWidget.PasswordInputWidget.TextInputWidget.TextProp.Set(password);
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
    }
}