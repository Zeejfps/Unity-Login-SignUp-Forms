using YADBF;

public interface IToggleWidget : IWidget, IInteractable
{
    ObservableProperty<bool> IsOnProp { get; }
    
    void HandleClick();
}