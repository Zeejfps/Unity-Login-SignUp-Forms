using Widgets;
using YADBF;

public sealed class LoginSignUpPageWidget : ILoginSignUpPageWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITabWidget LoginFormTabWidget { get; }
    public ITabWidget SignUpFormTabWidget { get; }
    
    public ILoginFormWidget LoginFormWidget { get; }
    public ISignUpFormWidget SignUpFormWidget { get; }
    
    public LoginSignUpPageWidget() {

        LoginFormWidget = new LoginFormWidget();
        SignUpFormWidget = new SignUpFormWidget();
        
        LoginFormTabWidget = new TabWidget();
        SignUpFormTabWidget = new TabWidget();
    }
}