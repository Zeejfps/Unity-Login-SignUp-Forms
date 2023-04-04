using YADBF;

internal sealed class LoginFormTabWidget : ITabWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsSelectedProp { get; } = new();

    private ILoginFormWidget LoginFormWidget { get; }
    private ISignUpFormWidgetController SignUpFormWidgetController { get; }

    public LoginFormTabWidget(ISignUpFormWidgetController signUpFormWidgetController, ILoginFormWidget loginFormWidget)
    {
        SignUpFormWidgetController = signUpFormWidgetController;
        LoginFormWidget = loginFormWidget;
        
        LoginFormWidget.IsVisibleProp.Bind(IsSelectedProp);
        IsSelectedProp.Bind(LoginFormWidget.IsVisibleProp);
    }

    public void HandleClick()
    {
        if (IsSelectedProp.Value)
            return;

        var email = SignUpFormWidgetController.Email;
        LoginFormWidget.EmailFieldWidget.TextInputWidget.TextProp.Set(email);
        IsSelectedProp.Set(true);
    }
}