using YADBF;

namespace Common.Widgets
{
    public sealed class TabWidget : ITabWidget
    {
        public ObservableProperty<bool> IsSelectedProp { get; } = new();
        public ObservableProperty<bool> IsVisibleProperty { get; } = new(true);

        public void HandleClick()
        {
            if (IsSelectedProp.IsFalse())
                IsSelectedProp.Set(true);
        }
    }
}