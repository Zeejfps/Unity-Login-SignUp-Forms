using Login;

internal sealed class SignUpFormUsernameInputWidget : BaseTextInputWidget
{
    public SignUpFormUsernameInputWidget(ISignUpFormWidgetController signUpFormWidget)
    {
        // TextProp = signUpForm.UsernameProp;
        // IsInteractableProperty.Bind(signUpForm.IsLoadingProp, value => !value);
    }
}