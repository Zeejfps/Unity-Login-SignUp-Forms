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
    
    public LoginSignUpWidget(
        ILoginService loginService,
        ISignUpService signUpService)
    {
        var emailValidator = new RegexEmailValidator();
        var passwordValidator = new PasswordRequirementsValidator(new IPasswordRequirement[]
        {
            new MinLengthPasswordRequirement(8),
            new MinDigitsPasswordRequirement(2),
            new MinUpperCaseCharactersPasswordRequirement(1),
            new MinLowerCaseCharactersPasswordRequirement(1),
            new MinSpecialCharactersPasswordRequirement(3)
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

        var controller = new LoginSignUpPageWidgetController(LoginFormWidgetController, SignUpFormWidgetController,
            LoginFormTabWidget);
    }
}