namespace Tests
{
    public abstract class SignUpFormWidgetControllerState : IState
    {
        protected ISignUpFormWidgetController SignUpFormWidgetController { get; }
        
        
        protected ITextInputWidget EmailInputWidget =>
            EmailFieldWidget.TextInputWidget;
        
        protected ITextFieldWidget EmailFieldWidget =>
            SignUpFormWidgetController.SignUpFormWidget.EmailFieldWidget;
        
        protected IPasswordFieldWidget PasswordFieldWidget =>
            SignUpFormWidgetController.SignUpFormWidget.PasswordFieldWidget;
        
        protected ITextInputWidget PasswordInputWidget =>
            PasswordFieldWidget.TextInputWidget;
        
        protected ITextInputWidget ConfirmPasswordInputWidget =>
            SignUpFormWidgetController.SignUpFormWidget.ConfirmPasswordFieldWidget.TextInputWidget;
        
        protected SignUpFormWidgetControllerState(ISignUpFormWidgetController signUpFormWidgetController)
        {
            SignUpFormWidgetController = signUpFormWidgetController;
        }

        public abstract void OnEntered();
        public abstract void OnExited();
    }
}