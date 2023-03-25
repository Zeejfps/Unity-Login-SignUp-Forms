internal sealed class LoginFormEmailInputWidget : BaseTextInputWidget
{
    public LoginFormEmailInputWidget(ILoginForm loginForm)
    {
        TextProp = loginForm.EmailProp;
        IsInteractableProperty.Bind(loginForm.IsLoadingProp, value => !value);
    }
}