internal sealed class LoginFormEmailTextInputWidget : BaseTextInputWidget
{
    public LoginFormEmailTextInputWidget(ILoginForm loginForm)
    {
        TextProp = loginForm.EmailProp;
        IsInteractableProperty.Bind(loginForm.IsLoadingProp, value => !value);
    }
}