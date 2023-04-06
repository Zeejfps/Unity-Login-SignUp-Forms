using System;
using System.Threading.Tasks;
using Login;
using YADBF;

namespace Common.Widgets
{
    public sealed class DefaultInfoPopupWidgetController : IWidgetController
    {
        private IInfoPopupWidget InfoPopupWidget { get; }

        public DefaultInfoPopupWidgetController(IInfoPopupWidget infoPopupWidget)
        {
            InfoPopupWidget = infoPopupWidget;
            InfoPopupWidget.OkButtonWidget.ActionProp.Set(Close);
            InfoPopupWidget.OkButtonWidget.IsInteractableProperty.Set(true);
        }

        public void Close()
        {
            InfoPopupWidget.IsVisibleProperty.Set(false);
        }
        
        public bool ProcessInputEvent(InputEvent inputEvent)
        {
            return false;
        }

        public void Dispose()
        {
        }

        public static async Task ShowAndWaitUntilClosed(IPopupManager popupService, IInfoPopupWidget infoPopupWidget)
        {
            var controller = new DefaultInfoPopupWidgetController(infoPopupWidget);
            await popupService.ShowPopupAsync(infoPopupWidget);
            controller.Dispose();
        }
    }
    
    internal sealed class InfoPopupWidget : IInfoPopupWidget
    {
        public ObservableProperty<string> TitleTextProp { get; } = new();
        public ObservableProperty<string> InfoTextProp { get; } = new();
        public ObservableProperty<bool> IsVisibleProperty { get; } = new(true);
        public IButtonWidget OkButtonWidget { get; } = new ButtonWidget();
    }
}