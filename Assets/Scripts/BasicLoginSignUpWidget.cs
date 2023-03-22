using YADBF;

public sealed class BasicLoginSignUpWidget : ILoginSignUpWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITabWidget LoginFormTabWidget { get; }
    public ITabWidget SignUpFormTabWidget { get; }
    public ILoginFormWidget LoginFormWidget { get; }
    public ISignUpFormWidget SignUpFormWidget { get; }

    public BasicLoginSignUpWidget(ILoginFormWidget loginFormWidget, ISignUpFormWidget signUpFormWidget)
    {
        LoginFormWidget = loginFormWidget;
        LoginFormTabWidget = new TabWidget(loginFormWidget);
        
        SignUpFormWidget = signUpFormWidget;
        SignUpFormTabWidget = new TabWidget(signUpFormWidget);

        var tabGroup = new TabGroup();
        tabGroup.AddTab(LoginFormTabWidget);
        tabGroup.AddTab(SignUpFormTabWidget);
        LoginFormTabWidget.IsSelectedProp.Set(true);
    }
}