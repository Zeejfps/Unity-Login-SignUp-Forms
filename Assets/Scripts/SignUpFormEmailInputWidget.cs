internal sealed class SignUpFormEmailInputWidget : BaseTextInputWidget
{
    public SignUpFormEmailInputWidget(ISignUpForm signUpForm)
    {
        TextProp = signUpForm.EmailProp;
        IsInteractableProperty.Bind(signUpForm.IsLoadingProp, value => !value);
    }
}