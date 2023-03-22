using YADBF;

public interface ITextInputWidget
{
    ObservableProperty<string> TextProp { get; }
    ObservableProperty<bool> IsInteractableProp { get; }
}