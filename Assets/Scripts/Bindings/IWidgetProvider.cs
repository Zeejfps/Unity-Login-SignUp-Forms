namespace Bindings
{
    public interface IWidgetProvider
    {
        public TWidget Get<TWidget>(string widgetId) where TWidget : class, IWidget;
    }
}