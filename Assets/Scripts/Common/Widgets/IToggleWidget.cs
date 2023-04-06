using YADBF;

namespace Common.Widgets
{
    public interface IToggleWidget : IWidget, IInteractable
    {
        ObservableProperty<bool> IsOnProp { get; }
    
        void HandleClick();
    }
}