using YADBF;

public interface ITextFieldWidget : IWidget
{
    ObservableProperty<string> ErrorTextProp { get; }
    ITextInputWidget TextInputWidget { get; }    
}