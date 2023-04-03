namespace Tests
{
    public abstract class SignUpFormWidgetControllerState : IState
    {
        protected ISignUpFormWidgetController SignUpFormWidgetController { get; }

        protected ITextInputWidget EmailInputWidget =>
            SignUpFormWidgetController.SignUpFormWidget.EmailFieldWidget.TextInputWidget;

        protected ITextInputWidget UsernameInputWidget =>
            SignUpFormWidgetController.SignUpFormWidget.UsernameFieldWidget.TextInputWidget;
        
        protected ITextInputWidget PasswordInputWidget =>
            SignUpFormWidgetController.SignUpFormWidget.PasswordFieldWidget.TextInputWidget;
        
        protected ITextInputWidget ConfirmPasswordInputWidget =>
            SignUpFormWidgetController.SignUpFormWidget.ConfirmPasswordFieldWidget.TextInputWidget;

        protected IButtonWidget SubmitButtonWidget => 
            SignUpFormWidgetController.SignUpFormWidget.SignUpButtonWidget;
        
        protected SignUpFormWidgetControllerState(ISignUpFormWidgetController signUpFormWidgetController)
        {
            SignUpFormWidgetController = signUpFormWidgetController;
        }

        public abstract void OnEntered();
        public abstract void OnExited();
    }
}