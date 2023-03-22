using YADBF;

namespace Login
{
    internal class BaseTextInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; } = new();
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();
    }
}