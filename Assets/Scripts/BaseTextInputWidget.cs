using YADBF;

public abstract class BaseTextInputWidget : ITextInputWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> TextProp { get; protected set; }
    public ObservableProperty<string> ErrorTextProperty { get; } = new();
    public ObservableProperty<bool> IsInteractableProperty { get; } = new();
    public ObservableProperty<bool> IsMaskingCharactersProperty { get; } = new();
}