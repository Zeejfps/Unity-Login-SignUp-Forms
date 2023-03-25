internal sealed class LoginFormPasswordInputWidget : BaseTextInputWidget
{
    public LoginFormPasswordInputWidget(ILoginForm loginForm)
    {
        TextProp = loginForm.PasswordProp;
        IsMaskingCharactersProperty.Set(true);
        IsInteractableProperty.Bind(loginForm.IsLoadingProp, value => !value);
    }
}