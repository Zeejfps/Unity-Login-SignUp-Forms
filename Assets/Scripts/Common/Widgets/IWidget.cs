using YADBF;

namespace Common.Widgets
{
    public interface IWidget
    {
        ObservableProperty<bool> IsVisibleProp { get; }
    }
}