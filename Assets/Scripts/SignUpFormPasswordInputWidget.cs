using Login;

internal sealed class SignUpFormPasswordInputWidget : BaseTextInputWidget
{
    public SignUpFormPasswordInputWidget(ISignUpForm signUpForm)
    {
        TextProp = signUpForm.PasswordProp;
        IsMaskingCharactersProperty.Set(true);
        IsInteractableProperty.Bind(signUpForm.IsLoadingProp, value => !value);
    }
}