using YADBF;

public interface IInteractableWidget
{
    ObservableProperty<bool> IsFocusedProperty { get; }
    ObservableProperty<bool> IsInteractableProperty { get; }
}