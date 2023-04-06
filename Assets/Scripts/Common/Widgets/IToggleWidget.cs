using YADBF;

namespace Common.Widgets
{
    public interface IToggleWidget : IWidget, IInteractableWidget
    {
        ObservableProperty<bool> IsOnProp { get; }
    
        void HandleClick();
    }
}