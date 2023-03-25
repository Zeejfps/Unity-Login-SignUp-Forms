using Login;
using YADBF;

public sealed class LoginSignUpWidget : ILoginSignUpWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ITabWidget LoginFormTabWidget { get; }
    public ITabWidget SignUpFormTabWidget { get; }
    public ILoginFormWidget LoginFormWidget { get; }
    public ISignUpFormWidget SignUpFormWidget { get; }

    private IPopupManager PopupManager { get; }
    private ISignUpConfirmationForm SignUpConfirmationForm { get; }
    
    public LoginSignUpWidget(
        ILoginForm loginForm, 
        ISignUpForm signUpForm, 
        ISignUpConfirmationForm signUpConfirmationForm,
        IPopupManager popupManager)
    {
        SignUpConfirmationForm = signUpConfirmationForm;
        PopupManager = popupManager;
        
        LoginFormWidget = new LoginFormWidget(
            new LoginFormEmailInputWidget(loginForm),
            new PasswordFieldWidget(new LoginFormPasswordInputWidget(loginForm)),
            new LoginFormLoginButton(loginForm));
        
        LoginFormTabWidget = new LoginFormTabWidget(signUpForm, LoginFormWidget);
        
        SignUpFormWidget = new SignUpFormWidget(
            new SignUpFormEmailInputWidget(signUpForm),
            new SignUpFormUsernameInputWidget(signUpForm),
            new PasswordFieldWidget(new SignUpFormPasswordInputWidget(signUpForm)),
            new PasswordFieldWidget(new SignUpFormConfirmPasswordInputWidget(signUpForm)),
            new SignUpFormSignUpButton(signUpForm));

        SignUpFormTabWidget = new SignUpFormTabWidget(loginForm, SignUpFormWidget);

        var tabGroup = new TabGroup();
        tabGroup.AddTab(LoginFormTabWidget);
        tabGroup.AddTab(SignUpFormTabWidget);
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
        signUpForm.Submitted += SignUpForm_OnSubmitted;
        signUpConfirmationForm.Submitted += SignUpConfirmationForm_OnSubmitted;
    }

    private void SignUpConfirmationForm_OnSubmitted(ISignUpConfirmationForm form)
    {
        var signUpFormWidget = SignUpFormWidget;
        var loginFormWidget = LoginFormWidget;
        
        var email = signUpFormWidget.EmailInputWidget.TextProp.Value;
        var password = signUpFormWidget.PasswordFieldWidget.TextInputWidget.TextProp.Value;

        loginFormWidget.EmailInputWidget.TextProp.Set(email);
        loginFormWidget.PasswordInputWidget.TextInputWidget.TextProp.Set(password);

        signUpFormWidget.PasswordFieldWidget.TextInputWidget.TextProp.Set(string.Empty);
        signUpFormWidget.ConfirmPasswordFieldWidget.TextInputWidget.TextProp.Set(string.Empty);
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
    }

    private async void SignUpForm_OnSubmitted()
    {
        await PopupManager.ShowPopupAsync(new ConfirmSignUpPopupWidget(SignUpConfirmationForm));
    }
}