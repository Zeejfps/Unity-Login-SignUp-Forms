using YADBF;

public sealed class LoginSignUpWidget : ILoginSignUpPageWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITabWidget LoginFormTabWidget { get; }
    public ITabWidget SignUpFormTabWidget { get; }
    
    public ILoginFormWidget LoginFormWidget { get; }
    public ISignUpFormWidget SignUpFormWidget { get; }
    
    public LoginSignUpWidget() {

        LoginFormWidget = new LoginFormWidget();
        SignUpFormWidget = new SignUpFormWidget();
        
        LoginFormTabWidget = new TabWidget();
        SignUpFormTabWidget = new TabWidget();
    }
}