using YADBF;

namespace Common.Widgets
{
    public sealed class TextFieldWidget : ITextFieldWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> ErrorTextProp { get; } = new(string.Empty);
        public ITextInputWidget TextInputWidget { get; } = new TextInputWidget();
    }
}