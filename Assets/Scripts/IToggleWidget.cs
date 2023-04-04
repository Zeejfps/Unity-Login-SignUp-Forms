using YADBF;

public interface IToggleWidget : IWidget
{
    ObservableProperty<bool> IsOnProp { get; }
    ObservableProperty<bool> IsInteractableProperty { get; }
    
    void HandleClick();
}