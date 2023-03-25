using YADBF;

public sealed class LoginFormRememberMeToggleWidget : IToggleWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsOnProp { get; } = new(true);
    public ObservableProperty<bool> IsInteractable { get; } = new();
    
    public void HandleClick()
    {
        IsOnProp.Toggle();
    }
}