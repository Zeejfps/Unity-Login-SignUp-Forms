using YADBF;

public interface ITextInputWidget : IWidget
{
    ObservableProperty<string> TextProp { get; }
    ObservableProperty<string> ErrorTextProperty { get; }
    ObservableProperty<bool> IsInteractableProperty { get; }
    ObservableProperty<bool> IsMaskingCharactersProperty { get; }
}