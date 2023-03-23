using Login;
using YADBF;

internal sealed class SignUpFormTabWidget : ITabWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsSelectedProp { get; } = new();

    private ILoginManager LoginManager { get; }
    private ISignUpFormWidget SignUpFormWidget { get; }
    
    public SignUpFormTabWidget(ILoginManager loginManager, ISignUpFormWidget signUpFormWidget)
    {
        LoginManager = loginManager;
        SignUpFormWidget = signUpFormWidget;
        
        SignUpFormWidget.IsVisibleProp.Bind(IsSelectedProp);
        IsSelectedProp.Bind(SignUpFormWidget.IsVisibleProp);
    }
    
    public void HandleClick()
    {
        if (IsSelectedProp.Value)
            return;
        
        var email = LoginManager.EmailProp.Value;
        if (!string.IsNullOrWhiteSpace(email))
            SignUpFormWidget.EmailInputWidget.TextProp.Set(email);
        
        IsSelectedProp.Set(true);
    }
}