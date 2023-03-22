using YADBF;

public interface IToggleWidget : IWidget
{
    ObservableProperty<bool> IsOnProp { get; }
    ObservableProperty<bool> IsInteractable { get; }
    void HandleClick();
}