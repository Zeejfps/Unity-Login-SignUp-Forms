using YADBF;

public interface ITextInputWidget : IWidget
{
    ObservableProperty<string> TextProp { get; }
    ObservableProperty<bool> IsInteractableProp { get; }
}