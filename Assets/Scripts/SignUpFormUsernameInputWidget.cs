using Login;

internal sealed class SignUpFormUsernameInputWidget : BaseTextInputWidget
{
    public SignUpFormUsernameInputWidget(ISignUpForm signUpForm)
    {
        TextProp = signUpForm.UsernameProp;
        IsInteractableProperty.Bind(signUpForm.IsLoadingProp, value => !value);
    }
}