using YADBF;

public sealed class LoginFormRememberMeToggleWidget : IToggleWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsOnProp { get; } = new(true);
    public ObservableProperty<bool> IsInteractable { get; } = new();

    private ILoginFormWidgetController LoginFormWidgetController { get; }
    
    public LoginFormRememberMeToggleWidget(ILoginFormWidgetController loginFormWidgetController)
    {
        LoginFormWidgetController = loginFormWidgetController;
        IsOnProp.ValueChanged += IsOnProp_OnValueChanged;
        LoginFormWidgetController.IsRememberMeChecked = IsOnProp.Value;
        IsInteractable.Bind(loginFormWidgetController.IsLoadingProp, value => !value);
    }

    private void IsOnProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
    {
        LoginFormWidgetController.IsRememberMeChecked = currvalue;
    }

    public void HandleClick()
    {
        IsOnProp.Toggle();
    }
}