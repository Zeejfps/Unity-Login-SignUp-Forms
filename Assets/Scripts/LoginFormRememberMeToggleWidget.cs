using YADBF;

public sealed class LoginFormRememberMeToggleWidget : IToggleWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsOnProp { get; } = new(true);
    public ObservableProperty<bool> IsInteractable { get; } = new();

    private ILoginForm LoginForm { get; }
    
    public LoginFormRememberMeToggleWidget(ILoginForm loginForm)
    {
        LoginForm = loginForm;
        IsOnProp.ValueChanged += IsOnProp_OnValueChanged;
        LoginForm.IsRememberMeChecked = IsOnProp.Value;
        IsInteractable.Bind(loginForm.IsLoadingProp, value => !value);
    }

    private void IsOnProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
    {
        LoginForm.IsRememberMeChecked = currvalue;
    }

    public void HandleClick()
    {
        IsOnProp.Toggle();
    }
}