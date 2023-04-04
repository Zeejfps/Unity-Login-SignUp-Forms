using Login;
using YADBF;

internal sealed class SignUpFormTabWidget : ITabWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsSelectedProp { get; } = new();

    private ILoginFormWidgetController LoginFormWidgetController { get; }
    private ISignUpFormWidget SignUpFormWidget { get; }
    
    public SignUpFormTabWidget(ILoginFormWidgetController loginFormWidgetController, ISignUpFormWidget signUpFormWidget)
    {
        LoginFormWidgetController = loginFormWidgetController;
        SignUpFormWidget = signUpFormWidget;
        
        SignUpFormWidget.IsVisibleProp.Bind(IsSelectedProp);
        IsSelectedProp.Bind(SignUpFormWidget.IsVisibleProp);
    }
    
    public void HandleClick()
    {
        if (IsSelectedProp.Value)
            return;
        
        var email = LoginFormWidgetController.Email;
        if (!string.IsNullOrWhiteSpace(email))
            SignUpFormWidget.EmailFieldWidget.TextInputWidget.TextProp.Set(email);
        
        IsSelectedProp.Set(true);
    }
}