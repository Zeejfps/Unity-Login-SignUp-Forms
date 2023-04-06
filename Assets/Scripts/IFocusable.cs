using YADBF;

public interface IFocusable
{
    ObservableProperty<bool> IsFocusedProperty { get; }
    ObservableProperty<bool> CanBeFocusedProperty { get; }
}