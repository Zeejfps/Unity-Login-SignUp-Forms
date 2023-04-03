using Tests;

public sealed class SignUpFormWidgetControllerSubmittingFormState : SignUpFormWidgetControllerState
{
    public SignUpFormWidgetControllerSubmittingFormState(ISignUpFormWidgetController signUpFormWidgetController) : base(signUpFormWidgetController)
    {
    }

    public override void OnEntered()
    {
        EmailInputWidget.IsInteractableProperty.Set(false);
        UsernameInputWidget.IsInteractableProperty.Set(false);
        PasswordInputWidget.IsInteractableProperty.Set(false);
        ConfirmPasswordInputWidget.IsInteractableProperty.Set(false);
        SubmitButtonWidget.IsInteractableProp.Set(false);
    }

    public override void OnExited()
    {
        
    }
}