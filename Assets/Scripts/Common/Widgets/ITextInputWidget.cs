using YADBF;

namespace Common.Widgets
{
    public interface ITextInputWidget : IWidget, IInteractableWidget
    {
        ObservableProperty<string> TextProp { get; }
        ObservableProperty<bool> IsMaskingCharactersProperty { get; }
    }
}