using Common.Controllers;
using Common.Widgets;
using Login;
using LoginForm;
using Services;
using SignUpForm;
using Validators;

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
            IPopupManager popupService,
            ILoginService loginService,
            ISignUpService signUpService,
            ILoginSignUpPageWidget loginSignUpPageWidget
        ) {
            LoginFormWidget = loginSignUpPageWidget.LoginFormWidget;
            SignUpFormWidget = loginSignUpPageWidget.SignUpFormWidget;
            LoginFormTabWidget = loginSignUpPageWidget.LoginFormTabWidget;
            SignUpFormTabWidget = loginSignUpPageWidget.SignUpFormTabWidget;
        
            var emailValidator = new RegexEmailValidator();
            var passwordValidators = new IPasswordValidator[]
            {
                new MinLengthPasswordValidator(3),
                new MinDigitsPasswordValidator(1),
                new MinUpperCaseCharactersPasswordValidator(1),
                new MinLowerCaseCharactersPasswordValidator(1),
                new MinSpecialCharactersPasswordValidator(1)
            };
        
            LoginFormWidgetPresenter = new LoginFormWidgetPresenter(popupService, loginService, emailValidator, LoginFormWidget);
            SignUpFormWidgetPresenter = new SignUpFormWidgetPresenter(signUpService, emailValidator, passwordValidators, SignUpFormWidget);
        
            SignUpFormWidgetPresenter.FormSubmitted += SignUpFormWidgetController_OnFormSubmitted;
        
            TabGroupController = new TabGroupController();
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
            LoginFormWidgetPresenter.Dispose();
            SignUpFormWidgetPresenter.Dispose();
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