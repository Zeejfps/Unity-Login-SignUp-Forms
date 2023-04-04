internal sealed class LoginFormEmailTextInputWidget : BaseTextInputWidget
{
    public LoginFormEmailTextInputWidget(ILoginFormWidgetController loginFormWidgetController)
    {
        TextProp = loginFormWidgetController.EmailProp;
        IsInteractableProperty.Bind(loginFormWidgetController.IsLoadingProp, value => !value);
    }
}