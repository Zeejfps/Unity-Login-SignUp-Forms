namespace SignUpForm
{
    public sealed class SignUpFormWidgetPresenterDefaultState : SignUpFormWidgetPresenterState
    {
        public SignUpFormWidgetPresenterDefaultState(ISignUpFormWidgetPresenter signUpFormWidgetPresenter) : base(signUpFormWidgetPresenter)
        {
        }

        public override void OnEntered()
        {
            SignUpFormWidgetPresenter.IsLoading = false;
            
            SignUpFormWidgetPresenter.EmailChanged += SignUpFormWidgetController_OnEmailChanged;
            SignUpFormWidgetPresenter.UsernameChanged += SignUpFormWidgetController_OnUsernameChanged;
            SignUpFormWidgetPresenter.PasswordChanged += SignUpFormWidgetController_OnPasswordChanged;
            SignUpFormWidgetPresenter.ConfirmPasswordChanged += SignUpFormWidgetController_OnConfirmPasswordChanged;
        }

        public override void OnExited()
        {
            SignUpFormWidgetPresenter.EmailChanged -= SignUpFormWidgetController_OnEmailChanged;
            SignUpFormWidgetPresenter.PasswordChanged -= SignUpFormWidgetController_OnPasswordChanged;
            SignUpFormWidgetPresenter.ConfirmPasswordChanged -= SignUpFormWidgetController_OnConfirmPasswordChanged;
        }

        private void SignUpFormWidgetController_OnEmailChanged()
        {
            SignUpFormWidgetPresenter.ValidateEmail();
        }

        private void SignUpFormWidgetController_OnUsernameChanged()
        {
            SignUpFormWidgetPresenter.ValidateUsername();
        }

        private void SignUpFormWidgetController_OnPasswordChanged()
        {
            SignUpFormWidgetPresenter.ConfirmPassword = string.Empty;
            SignUpFormWidgetPresenter.ValidatePassword();
        }

        private void SignUpFormWidgetController_OnConfirmPasswordChanged()
        {
            SignUpFormWidgetPresenter.ValidateConfirmPassword();
        }
    }
}