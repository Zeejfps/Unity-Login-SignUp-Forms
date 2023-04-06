using YADBF;

namespace Common.Widgets
{
    public sealed class TextFieldWidget : ITextFieldWidget
    {
        public ObservableProperty<bool> IsVisibleProperty { get; } = new(true);
        public ObservableProperty<string> ErrorTextProp { get; } = new(string.Empty);
        public ITextInputWidget TextInputWidget { get; } = new TextInputWidget();
    }
}