using Tests;

namespace SignUpForm
{
    public abstract class SignUpFormWidgetControllerState : IState
    {
        protected ISignUpFormWidgetController SignUpFormWidgetController { get; }
        
        protected SignUpFormWidgetControllerState(ISignUpFormWidgetController signUpFormWidgetController)
        {
            SignUpFormWidgetController = signUpFormWidgetController;
        }

        public abstract void OnEntered();
        public abstract void OnExited();
    }
}