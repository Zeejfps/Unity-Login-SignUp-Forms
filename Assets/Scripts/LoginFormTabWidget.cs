using Login;
using YADBF;

internal sealed class LoginFormTabWidget : ITabWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsSelectedProp { get; } = new();
    
    private ISignUpFormController SignUpManager { get; }
    private ILoginFormWidget LoginFormWidget { get; }

    public LoginFormTabWidget(ISignUpFormController signUpManager, ILoginFormWidget loginFormWidget)
    {
        SignUpManager = signUpManager;
        LoginFormWidget = loginFormWidget;
        
        LoginFormWidget.IsVisibleProp.Bind(IsSelectedProp);
        IsSelectedProp.Bind(LoginFormWidget.IsVisibleProp);
    }

    public void HandleClick()
    {
        if (IsSelectedProp.Value)
            return;

        // var email = SignUpManager.EmailProp.Value;
        // LoginFormWidget.EmailFieldWidget.TextInputWidget.TextProp.Set(email);
        // IsSelectedProp.Set(true);
    }
}