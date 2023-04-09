using Tests;

namespace SignUpForm
{
    public abstract class SignUpFormWidgetPresenterState : IState
    {
        protected ISignUpFormWidgetPresenter SignUpFormWidgetPresenter { get; }
        
        protected SignUpFormWidgetPresenterState(ISignUpFormWidgetPresenter signUpFormWidgetPresenter)
        {
            SignUpFormWidgetPresenter = signUpFormWidgetPresenter;
        }

        public abstract void OnEntered();
        public abstract void OnExited();
    }
}