public sealed class SignUpFormWidgetControllerDefaultState : SignUpFormWidgetControllerState
{
    public SignUpFormWidgetControllerDefaultState(ISignUpFormWidgetController signUpFormWidgetController) : base(signUpFormWidgetController)
    {
    }

    public override void OnEntered()
    {
        SignUpFormWidgetController.IsLoading = false;
            
        SignUpFormWidgetController.EmailChanged += SignUpFormWidgetController_OnEmailChanged;
        SignUpFormWidgetController.UsernameChanged += SignUpFormWidgetController_OnUsernameChanged;
        SignUpFormWidgetController.PasswordChanged += SignUpFormWidgetController_OnPasswordChanged;
        SignUpFormWidgetController.ConfirmPasswordChanged += SignUpFormWidgetController_OnConfirmPasswordChanged;
    }

    public override void OnExited()
    {
        SignUpFormWidgetController.EmailChanged -= SignUpFormWidgetController_OnEmailChanged;
        SignUpFormWidgetController.PasswordChanged -= SignUpFormWidgetController_OnPasswordChanged;
        SignUpFormWidgetController.ConfirmPasswordChanged -= SignUpFormWidgetController_OnConfirmPasswordChanged;
    }

    private void SignUpFormWidgetController_OnEmailChanged()
    {
        SignUpFormWidgetController.ValidateEmail();
    }

    private void SignUpFormWidgetController_OnUsernameChanged()
    {
        SignUpFormWidgetController.ValidateUsername();
    }

    private void SignUpFormWidgetController_OnPasswordChanged()
    {
        SignUpFormWidgetController.ConfirmPassword = string.Empty;
        SignUpFormWidgetController.ValidatePassword();
    }

    private void SignUpFormWidgetController_OnConfirmPasswordChanged()
    {
        SignUpFormWidgetController.ValidateConfirmPassword();
    }
}