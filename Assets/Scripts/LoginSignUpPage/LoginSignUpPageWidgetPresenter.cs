using Common.Controllers;
using Common.Widgets;
using Login;
using LoginForm;
using Services;
using SignUpForm;

namespace LoginSignUpPage
{
    public sealed class LoginSignUpPageWidgetPresenter : ILoginSignUpPageWidgetPresenter
    {
        private ILoginFormWidgetPresenter LoginFormWidgetPresenter { get; }
        private ISignUpFormWidgetPresenter SignUpFormWidgetPresenter { get; }
    
        private ILoginFormWidget LoginFormWidget { get; }
        private ISignUpFormWidget SignUpFormWidget { get; }
        private ITabWidget LoginFormTabWidget { get; }
        private ITabWidget SignUpFormTabWidget { get; }

        private ITabGroupController TabGroupController { get; }
    
        public LoginSignUpPageWidgetPresenter(
            ILoginSignUpPageWidget loginSignUpPageWidget,
            ILoginFormWidgetPresenter loginFormWidgetPresenter,
            ISignUpFormWidgetPresenter signUpFormWidgetPresenter
        ) {
            LoginFormWidget = loginSignUpPageWidget.LoginFormWidget;
            SignUpFormWidget = loginSignUpPageWidget.SignUpFormWidget;
            LoginFormTabWidget = loginSignUpPageWidget.LoginFormTabWidget;
            SignUpFormTabWidget = loginSignUpPageWidget.SignUpFormTabWidget;
            
            LoginFormWidgetPresenter = loginFormWidgetPresenter;
            SignUpFormWidgetPresenter = signUpFormWidgetPresenter;
        
            SignUpFormWidgetPresenter.FormSubmitted += SignUpFormWidgetController_OnFormSubmitted;
        
            TabGroupController = new TabGroup();
            TabGroupController.LinkTabToContent(LoginFormTabWidget, LoginFormWidget);
            TabGroupController.LinkTabToContent(SignUpFormTabWidget, SignUpFormWidget);
        
            LoginFormTabWidget.IsSelectedProp.Set(true);  
        }

        public bool ProcessInputEvent(InputEvent inputEvent)
        {
            if (LoginFormWidgetPresenter.ProcessInputEvent(inputEvent)) 
                return true;
        
            if (SignUpFormWidgetPresenter.ProcessInputEvent(inputEvent))
                return true;

            return false;
        }

        public void Dispose()
        {
            TabGroupController.Dispose();
        }
    
        private void SignUpFormWidgetController_OnFormSubmitted()
        {
            var email = SignUpFormWidgetPresenter.Email;
            var password = SignUpFormWidgetPresenter.Password;

            LoginFormWidgetPresenter.Email = email;
            LoginFormWidgetPresenter.Password = password;

            SignUpFormWidgetPresenter.Password = string.Empty;
            SignUpFormWidgetPresenter.ConfirmPassword = string.Empty;
        
            LoginFormTabWidget.IsSelectedProp.Set(true);
        }
    }
}