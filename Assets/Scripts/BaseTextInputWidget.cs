using YADBF;

namespace Login
{
    internal abstract class BasePasswordInputWidget : IPasswordInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ITextInputWidget TextInputWidget { get; }
        public IToggleWidget ShowPasswordToggleWidget { get; }
    }
    
    internal abstract class BaseTextInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; } = new();
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();
    }
}