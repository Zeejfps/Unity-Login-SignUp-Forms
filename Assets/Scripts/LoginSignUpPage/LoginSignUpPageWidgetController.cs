using Common.Controllers;
using Common.Widgets;
using Login;
using LoginForm;
using Services;
using SignUpForm;
using Validators;

namespace LoginSignUpPage
{
    public sealed class LoginSignUpPageWidgetController : ILoginSignUpPageWidgetController
    {
        private ILoginFormWidgetController LoginFormWidgetController { get; }
        private ISignUpFormWidgetController SignUpFormWidgetController { get; }
    
        private ILoginFormWidget LoginFormWidget { get; }
        private ISignUpFormWidget SignUpFormWidget { get; }
        private ITabWidget LoginFormTabWidget { get; }
        private ITabWidget SignUpFormTabWidget { get; }

        private ITabGroupController TabGroupController { get; }
    
        public LoginSignUpPageWidgetController(
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
        
            LoginFormWidgetController = new LoginFormWidgetController(popupService, loginService, emailValidator, LoginFormWidget);
            SignUpFormWidgetController = new SignUpFormWidgetController(signUpService, emailValidator, passwordValidators, SignUpFormWidget);
        
            SignUpFormWidgetController.FormSubmitted += SignUpFormWidgetController_OnFormSubmitted;
        
            TabGroupController = new TabGroupController();
            TabGroupController.LinkTabToContent(LoginFormTabWidget, LoginFormWidget);
            TabGroupController.LinkTabToContent(SignUpFormTabWidget, SignUpFormWidget);
        
            LoginFormTabWidget.IsSelectedProp.Set(true);  
        }

        public bool ProcessInputEvent(InputEvent inputEvent)
        {
            if (LoginFormWidgetController.ProcessInputEvent(inputEvent)) 
                return true;
        
            if (SignUpFormWidgetController.ProcessInputEvent(inputEvent))
                return true;

            return false;
        }

        public void Dispose()
        {
            LoginFormWidgetController.Dispose();
            SignUpFormWidgetController.Dispose();
            TabGroupController.Dispose();
        }
    
        private void SignUpFormWidgetController_OnFormSubmitted()
        {
            var email = SignUpFormWidgetController.Email;
            var password = SignUpFormWidgetController.Password;

            LoginFormWidgetController.Email = email;
            LoginFormWidgetController.Password = password;

            SignUpFormWidgetController.Password = string.Empty;
            SignUpFormWidgetController.ConfirmPassword = string.Empty;
        
            LoginFormTabWidget.IsSelectedProp.Set(true);
        }
    }
}