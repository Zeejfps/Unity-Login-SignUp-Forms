using System;
using YADBF;

public sealed class ToggleWidget : IToggleWidget
{
    public event Action<IToggleWidget> Clicked;
    
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsOnProp { get; } = new();
    public ObservableProperty<bool> IsInteractable { get; } = new();
    
    public void HandleClick()
    {
        Clicked?.Invoke(this);
    }
}