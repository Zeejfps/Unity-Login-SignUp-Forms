
public sealed class LoginSignUpPageWidgetController
{
    private ILoginFormWidgetController LoginFormWidgetController { get; }
    private ISignUpFormWidgetController SignUpFormWidgetController { get; }
    
    private ITabWidget LoginFormTabWidget { get; }

    public LoginSignUpPageWidgetController(
        ILoginFormWidgetController loginFormWidgetController, 
        ISignUpFormWidgetController signUpFormWidgetController,
        ITabWidget loginFormTabWidget
    ) {
        LoginFormWidgetController = loginFormWidgetController;
        SignUpFormWidgetController = signUpFormWidgetController;
        LoginFormTabWidget = loginFormTabWidget;
        
        SignUpFormWidgetController.FormSubmitted += SignUpFormWidgetController_OnFormSubmitted;
        LoginFormTabWidget.IsSelectedProp.Set(true);
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