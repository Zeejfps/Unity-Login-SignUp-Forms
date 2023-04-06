namespace SignUpForm
{
    public sealed class SignUpFormWidgetControllerSubmittingFormState : SignUpFormWidgetControllerState
    {
        public SignUpFormWidgetControllerSubmittingFormState(ISignUpFormWidgetController signUpFormWidgetController) : base(signUpFormWidgetController)
        {
        }

        public override void OnEntered()
        {
            SignUpFormWidgetController.IsLoading = true;
        }

        public override void OnExited()
        {
        
        }
    }
}