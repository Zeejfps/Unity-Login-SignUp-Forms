namespace Tests
{
    public sealed class SignUpFormWidgetControllerDefaultState : SignUpFormWidgetControllerState
    {
        private ITextInputWidget EmailInputWidget =>
            SignUpFormWidgetController.SignUpFormWidget.EmailFieldWidget.TextInputWidget;

        private ITextInputWidget UsernameInputWidget =>
            SignUpFormWidgetController.SignUpFormWidget.UsernameFieldWidget.TextInputWidget;
        
        private ITextInputWidget PasswordInputWidget =>
            SignUpFormWidgetController.SignUpFormWidget.PasswordFieldWidget.TextInputWidget;
        
        private ITextInputWidget ConfirmPasswordInputWidget =>
            SignUpFormWidgetController.SignUpFormWidget.ConfirmPasswordFieldWidget.TextInputWidget;

        private IButtonWidget SubmitButtonWidget => 
            SignUpFormWidgetController.SignUpFormWidget.SignUpButtonWidget;
        
        public SignUpFormWidgetControllerDefaultState(ISignUpFormWidgetController signUpFormWidgetController) : base(signUpFormWidgetController)
        {
        }

        public override void OnEntered()
        {
            EmailInputWidget.IsInteractableProperty.Set(true);
            UsernameInputWidget.IsInteractableProperty.Set(true);
            PasswordInputWidget.IsInteractableProperty.Set(true);
            ConfirmPasswordInputWidget.IsInteractableProperty.Set(true);
            SubmitButtonWidget.ActionProp.Set(Submit);
        }

        public override void OnExited()
        {
            
        }

        private void Submit()
        {
            
        }
    }
}