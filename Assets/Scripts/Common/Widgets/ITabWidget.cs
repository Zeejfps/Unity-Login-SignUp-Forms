using YADBF;

namespace Common.Widgets
{
    public interface ITabWidget : IWidget
    {
        ObservableProperty<bool> IsSelectedProp { get; }

        void HandleClick();
    }
}