namespace SignUpForm
{
    public sealed class SignUpFormWidgetPresenterSubmittingFormState : SignUpFormWidgetPresenterState
    {
        public SignUpFormWidgetPresenterSubmittingFormState(ISignUpFormWidgetPresenter signUpFormWidgetPresenter) : base(signUpFormWidgetPresenter)
        {
        }

        public override void OnEntered()
        {
            SignUpFormWidgetPresenter.IsLoading = true;
        }

        public override void OnExited()
        {
        
        }
    }
}