using YADBF;

namespace Common.Widgets
{
    public interface ITextFieldWidget : IWidget
    {
        ObservableProperty<string> ErrorTextProp { get; }
        ITextInputWidget TextInputWidget { get; }    
    }
}