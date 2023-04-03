using Login;

internal sealed class SignUpFormUsernameInputWidget : BaseTextInputWidget
{
    public SignUpFormUsernameInputWidget(ISignUpFormController signUpForm)
    {
        // TextProp = signUpForm.UsernameProp;
        // IsInteractableProperty.Bind(signUpForm.IsLoadingProp, value => !value);
    }
}