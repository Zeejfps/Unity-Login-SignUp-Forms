namespace Tests
{
    public sealed class SignUpFormWidgetControllerDefaultState : SignUpFormWidgetControllerState
    {
        public SignUpFormWidgetControllerDefaultState(ISignUpFormWidgetController signUpFormWidgetController) : base(signUpFormWidgetController)
        {
        }

        public override void OnEntered()
        {
            EmailInputWidget.IsInteractableProperty.Set(true);
            UsernameInputWidget.IsInteractableProperty.Set(true);
            PasswordInputWidget.IsInteractableProperty.Set(true);
            ConfirmPasswordInputWidget.IsInteractableProperty.Set(true);
            
            SubmitButtonWidget.ActionProp.Set(SignUpFormWidgetController.Submit);
            SubmitButtonWidget.IsInteractableProp.Set(true);
            SubmitButtonWidget.IsLoadingProp.Set(false);
        }

        public override void OnExited()
        {
            
        }
    }
}