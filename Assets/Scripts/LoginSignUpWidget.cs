using Login;
using YADBF;

public sealed class LoginSignUpWidget : ILoginSignUpWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITabWidget LoginFormTabWidget { get; }
    public ITabWidget SignUpFormTabWidget { get; }
    
    public ILoginFormWidget LoginFormWidget { get; }
    public ISignUpFormWidget SignUpFormWidget { get; }

    private ILoginFormWidgetController LoginFormWidgetController { get; }
    private ISignUpFormWidgetController SignUpFormWidgetController { get; }
    private IPopupManager PopupManager { get; }
    private ISignUpConfirmationForm SignUpConfirmationForm { get; }
    
    public LoginSignUpWidget(
        ILoginService loginService,
        ISignUpService signUpService,
        ISignUpConfirmationForm signUpConfirmationForm,
        IPopupManager popupManager)
    {
        SignUpConfirmationForm = signUpConfirmationForm;
        PopupManager = popupManager;
        
        var emailValidator = new RegexEmailValidator();
        var passwordValidator = new PasswordRequirementsValidator(new IPasswordRequirement[]
        {
            new MinLengthPasswordRequirement(3),
            new MinDigitsPasswordRequirement(1),
            new MinUpperCaseCharactersPasswordRequirement(1),
            new MinLowerCaseCharactersPasswordRequirement(1),
            new MinSpecialCharactersPasswordRequirement(1)
        });
        
        LoginFormWidget = new LoginFormWidget();
        LoginFormWidgetController = new LoginFormWidgetController(loginService, emailValidator, LoginFormWidget);
        
        SignUpFormWidget = new SignUpFormWidget();
        SignUpFormTabWidget = new SignUpFormTabWidget(LoginFormWidgetController, SignUpFormWidget);
        
        SignUpFormWidgetController = new SignUpFormWidgetController(signUpService, emailValidator, passwordValidator, SignUpFormWidget);
        LoginFormTabWidget = new LoginFormTabWidget(SignUpFormWidgetController, LoginFormWidget);

        var tabGroup = new TabGroup();
        tabGroup.AddTab(LoginFormTabWidget);
        tabGroup.AddTab(SignUpFormTabWidget);
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
        SignUpFormWidgetController.FormSubmitted += SignUpFormWidgetController_OnFormSubmitted;
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