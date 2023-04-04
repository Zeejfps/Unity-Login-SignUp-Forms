using System.Collections.Generic;
using Login;
using Tests;
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
        ILoginFormWidgetController loginFormWidgetController, 
        ISignUpService signUpService,
        ISignUpConfirmationForm signUpConfirmationForm,
        IPopupManager popupManager)
    {
        SignUpConfirmationForm = signUpConfirmationForm;
        PopupManager = popupManager;
        
        LoginFormWidget = new LoginFormWidget(
            loginFormWidgetController,
            new PasswordFieldWidget(new LoginFormPasswordInputWidget(loginFormWidgetController)),
            new LoginFormSubmitButton(loginFormWidgetController));

        SignUpFormWidget = new SignUpFormWidget();
        SignUpFormTabWidget = new SignUpFormTabWidget(loginFormWidgetController, SignUpFormWidget);

        var emailValidator = new RegexEmailValidator();
        var passwordValidator = new PasswordRequirementsValidator(new IPasswordRequirement[]
        {
            new MinLengthPasswordRequirement(3),
            new MinDigitsPasswordRequirement(1),
            new MinUpperCaseCharactersPasswordRequirement(1),
            new MinLowerCaseCharactersPasswordRequirement(1),
            new MinSpecialCharactersPasswordRequirement(1)
        });
        var signUpFormController = new SignUpFormWidgetController(signUpService, emailValidator, passwordValidator, SignUpFormWidget);
        LoginFormTabWidget = new LoginFormTabWidget(signUpFormController, LoginFormWidget);

        var tabGroup = new TabGroup();
        tabGroup.AddTab(LoginFormTabWidget);
        tabGroup.AddTab(SignUpFormTabWidget);
        
        LoginFormTabWidget.IsSelectedProp.Set(true);
        signUpFormController.FormSubmitted += SignUpForm_OnSubmitted;
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