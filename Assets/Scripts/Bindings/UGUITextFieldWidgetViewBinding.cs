using Common.Widgets;
using UGUI;

namespace Bindings
{
    public sealed class UGUITextFieldWidgetViewBinding : WidgetViewBinding<UGUITextFieldWidgetView, ITextFieldWidget>
    {
        
    }

    public interface IWidgetProvider
    {
        public TWidget Get<TWidget>(string widgetId) where TWidget : class, IWidget;
    }
}
