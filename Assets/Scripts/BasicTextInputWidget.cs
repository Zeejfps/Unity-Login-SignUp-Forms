using YADBF;

namespace Login
{
    internal sealed class BasicTextInputWidget : ITextInputWidget
    {
        public ObservableProperty<string> TextProp { get; } = new();
        public ObservableProperty<bool> IsInteractableProp { get; } = new(true);
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    }
}