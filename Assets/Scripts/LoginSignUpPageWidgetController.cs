
using Login;
using YADBF;

public sealed class LoginSignUpPageWidgetController
{
    private ILoginFormWidgetController LoginFormWidgetController { get; }
    private ISignUpFormWidgetController SignUpFormWidgetController { get; }
    
    private ILoginFormWidget LoginFormWidget { get; }
    private ISignUpFormWidget SignUpFormWidget { get; }
    private ITabWidget LoginFormTabWidget { get; }
    private ITabWidget SignUpFormTabWidget { get; }

    public LoginSignUpPageWidgetController(
        ILoginService loginService,
        ISignUpService signUpService,
        ILoginSignUpPageWidget loginSignUpPageWidget
    ) {
        LoginFormWidget = loginSignUpPageWidget.LoginFormWidget;
        SignUpFormWidget = loginSignUpPageWidget.SignUpFormWidget;
        LoginFormTabWidget = loginSignUpPageWidget.LoginFormTabWidget;
        SignUpFormTabWidget = loginSignUpPageWidget.SignUpFormTabWidget;
        
        var emailValidator = new RegexEmailValidator();
        var passwordValidator = new PasswordRequirementsValidator(new IPasswordRequirement[]
        {
            new MinLengthPasswordRequirement(3),
            new MinDigitsPasswordRequirement(1),
            new MinUpperCaseCharactersPasswordRequirement(1),
            new MinLowerCaseCharactersPasswordRequirement(1),
            new MinSpecialCharactersPasswordRequirement(1)
        });
        
        LoginFormWidgetController = new LoginFormWidgetController(loginService, emailValidator, LoginFormWidget);
        SignUpFormWidgetController = new SignUpFormWidgetController(signUpService, emailValidator, passwordValidator, SignUpFormWidget);
        
        LoginFormTabWidget.IsSelectedProp.ValueChanged += LoginFormTabWidget_IsSelectedProp_OnValueChanged;
        SignUpFormTabWidget.IsSelectedProp.ValueChanged += SignUpFormTabWidget_IsSelectedProp_OnValueChanged;
        
        SignUpFormWidgetController.FormSubmitted += SignUpFormWidgetController_OnFormSubmitted;
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
    }

    private void LoginFormTabWidget_IsSelectedProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isSelected)
    {
        if (isSelected)
        {
            LoginFormWidget.IsVisibleProp.Set(true);
            SignUpFormWidget.IsVisibleProp.Set(false);
        }
    }

    private void SignUpFormTabWidget_IsSelectedProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isSelected)
    {
        if (isSelected)
        {
            SignUpFormWidget.IsVisibleProp.Set(true);
            LoginFormWidget.IsVisibleProp.Set(false);
        }
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