using YADBF;

public sealed class TextInputWidget : ITextInputWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsFocusedProperty { get; } = new();
    public ObservableProperty<string> TextProp { get; } = new();
    public ObservableProperty<bool> IsInteractableProperty { get; } = new();
    public ObservableProperty<bool> IsMaskingCharactersProperty { get; } = new();
}