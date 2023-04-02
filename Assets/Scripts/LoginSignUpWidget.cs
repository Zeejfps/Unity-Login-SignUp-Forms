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
            loginForm,
            new PasswordFieldWidget(new LoginFormPasswordInputWidget(loginForm)),
            new LoginFormSubmitButton(loginForm));
        
        LoginFormTabWidget = new LoginFormTabWidget(signUpForm, LoginFormWidget);

        SignUpFormWidget = new SignUpFormWidget(
            new SignUpFormEmailFieldWidget(signUpForm),
            new SignUpFormUsernameFieldWidget(signUpForm),
            new PasswordFieldWidget(new SignUpFormPasswordInputWidget(signUpForm)),
            new SignUpFormConfirmPasswordFieldWidget(signUpForm),
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
        
        var email = signUpFormWidget.EmailFieldWidget.TextInputWidget.TextProp.Value;
        var password = signUpFormWidget.PasswordFieldWidget.TextInputWidget.TextProp.Value;

        loginFormWidget.EmailFieldWidget.TextInputWidget.TextProp.Set(email);
        loginFormWidget.PasswordFieldWidget.TextInputWidget.TextProp.Set(password);

        signUpFormWidget.PasswordFieldWidget.TextInputWidget.TextProp.Set(string.Empty);
        signUpFormWidget.ConfirmPasswordFieldWidget.TextInputWidget.TextProp.Set(string.Empty);
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
    }

    private async void SignUpForm_OnSubmitted()
    {
        await PopupManager.ShowPopupAsync(new ConfirmSignUpPopupWidget(SignUpConfirmationForm));
    }
}