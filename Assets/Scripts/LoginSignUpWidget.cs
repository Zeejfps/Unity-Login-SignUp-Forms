using Login;
using YADBF;

public sealed class LoginSignUpWidget : ILoginSignUpWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITabWidget LoginFormTabWidget { get; }
    public ITabWidget SignUpFormTabWidget { get; }
    public ILoginFormWidget LoginFormWidget { get; }
    public ISignUpFormWidget SignUpFormWidget { get; }

    public LoginSignUpWidget(ILoginManager loginManager, ISignUpFlow signUpFlow)
    {
        LoginFormWidget = new LoginFormWidget(loginManager);
        LoginFormTabWidget = new LoginFormTabWidget(signUpFlow, LoginFormWidget);
        
        SignUpFormWidget = new SignUpFormWidget(signUpFlow);
        SignUpFormTabWidget = new SignUpFormTabWidget(loginManager, SignUpFormWidget);

        var tabGroup = new TabGroup();
        tabGroup.AddTab(LoginFormTabWidget);
        tabGroup.AddTab(SignUpFormTabWidget);
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
        signUpFlow.Completed += SignUpFlow_OnCompleted; 
    }

    private void SignUpFlow_OnCompleted()
    {
        LoginFormTabWidget.IsSelectedProp.Set(true);
    }
}