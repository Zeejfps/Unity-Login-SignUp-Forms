using Login;

internal class SignUpFormConfirmPasswordInputWidget : BaseTextInputWidget
{
    public SignUpFormConfirmPasswordInputWidget(ISignUpForm signUpForm)
    {
        TextProp = signUpForm.ConfirmPasswordProp;
        IsMaskingCharactersProperty.Set(true);
        IsInteractableProperty.Bind(signUpForm.IsLoadingProp, value => !value);   
    }
}