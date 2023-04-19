using Common.Widgets;
using LoginForm;
using SignUpForm;
using YADBF;

namespace LoginSignUpPage
{
    public sealed class LoginSignUpPageWidget : ILoginSignUpPageWidget
    {
        public ObservableProperty<bool> IsVisibleProperty { get; } = new();
        public ITabWidget LoginFormTabWidget { get; }
        public ITabWidget SignUpFormTabWidget { get; }
    
        public ILoginFormWidget LoginFormWidget { get; }
        public ISignUpFormWidget SignUpFormWidget { get; }
    
        public LoginSignUpPageWidget(ILoginFormWidget loginFormWidget, ISignUpFormWidget signUpFormWidget) {

            LoginFormWidget = loginFormWidget;
            SignUpFormWidget = signUpFormWidget;
        
            LoginFormTabWidget = new TabWidget();
            SignUpFormTabWidget = new TabWidget();
        }
    }
}