using YADBF;

public interface IInteractable
{
    ObservableProperty<bool> IsFocusedProperty { get; }
    ObservableProperty<bool> IsInteractableProperty { get; }
}