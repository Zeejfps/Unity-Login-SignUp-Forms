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
        LoginFormWidget = new LoginFormWidget(loginFlow);
        LoginFormTabWidget = new LoginFormTabWidget(signUpFlow, LoginFormWidget);
        
        SignUpFormWidget = new SignUpFormWidget(signUpFlow);
        SignUpFormTabWidget = new SignUpFormTabWidget(loginFlow, SignUpFormWidget);

        var tabGroup = new TabGroup();
        tabGroup.AddTab(LoginFormTabWidget);
        tabGroup.AddTab(SignUpFormTabWidget);
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
        signUpFlow.Completed += SignUpFlow_OnCompleted; 
    }

    private void SignUpFlow_OnCompleted()
    {
        var email = SignUpFormWidget.EmailInputWidget.TextProp.Value;
        var password = SignUpFormWidget.PasswordFieldWidget.TextInputWidget.TextProp.Value;
        LoginFormWidget.EmailInputWidget.TextProp.Set(email);
        LoginFormWidget.PasswordInputWidget.TextInputWidget.TextProp.Set(password);
        LoginFormTabWidget.IsSelectedProp.Set(true);
    }
}