using Login;
using YADBF;

internal sealed class SignUpFormTabWidget : ITabWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsSelectedProp { get; } = new();

    private ILoginForm LoginForm { get; }
    private ISignUpFormWidget SignUpFormWidget { get; }
    
    public SignUpFormTabWidget(ILoginForm loginForm, ISignUpFormWidget signUpFormWidget)
    {
        LoginForm = loginForm;
        SignUpFormWidget = signUpFormWidget;
        
        SignUpFormWidget.IsVisibleProp.Bind(IsSelectedProp);
        IsSelectedProp.Bind(SignUpFormWidget.IsVisibleProp);
    }
    
    public void HandleClick()
    {
        if (IsSelectedProp.Value)
            return;
        
        var email = LoginForm.EmailProp.Value;
        if (!string.IsNullOrWhiteSpace(email))
            SignUpFormWidget.EmailInputWidget.TextProp.Set(email);
        
        IsSelectedProp.Set(true);
    }
}