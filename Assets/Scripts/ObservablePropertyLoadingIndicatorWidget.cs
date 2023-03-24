using YADBF;

namespace Login
{
    internal sealed class ObservablePropertyLoadingIndicatorWidget : ILoadingIndicatorWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; }

        public ObservablePropertyLoadingIndicatorWidget(ObservableProperty<bool> isVisibleProp)
        {
            IsVisibleProp = isVisibleProp;
        }
    }
}