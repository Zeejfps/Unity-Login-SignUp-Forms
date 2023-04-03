namespace Tests
{
    public abstract class SignUpFormWidgetControllerState : IState
    {
        protected ISignUpFormWidgetController SignUpFormWidgetController { get; }

        protected SignUpFormWidgetControllerState(ISignUpFormWidgetController signUpFormWidgetController)
        {
            SignUpFormWidgetController = signUpFormWidgetController;
        }

        public abstract void OnExited();
        public abstract void OnEntered();
    }
}