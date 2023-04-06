using YADBF;

public interface ITextInputWidget : IWidget, IInteractable
{
    ObservableProperty<string> TextProp { get; }
    ObservableProperty<bool> IsMaskingCharactersProperty { get; }
}