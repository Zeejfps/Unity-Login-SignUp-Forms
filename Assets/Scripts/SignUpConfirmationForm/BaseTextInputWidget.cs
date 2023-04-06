using YADBF;

namespace SignUpConfirmationForm
{
    public abstract class BaseTextInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsFocusedProperty { get; } = new();
        public ObservableProperty<string> TextProp { get; protected set; }
        public ObservableProperty<bool> IsInteractableProperty { get; } = new();
        public ObservableProperty<bool> IsMaskingCharactersProperty { get; } = new();
    }
}