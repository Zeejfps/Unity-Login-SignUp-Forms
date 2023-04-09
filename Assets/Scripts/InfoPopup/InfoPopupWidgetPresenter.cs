using System.Threading.Tasks;
using Login;

namespace InfoPopup
{
    public sealed class InfoPopupWidgetPresenter : IInfoPopupWidgetPresenter
    {
        private IPopupManager PopupService { get; }
        private IInfoPopupWidget InfoPopupWidget { get; }

        public InfoPopupWidgetPresenter(IPopupManager popupService)
        {
            PopupService = popupService;
            InfoPopupWidget = new InfoPopupWidget();
            InfoPopupWidget.OkButtonWidget.ActionProp.Set(Close);
            InfoPopupWidget.OkButtonWidget.IsInteractableProperty.Set(true);
        }

        private void Close()
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

        public async Task ShowAndWaitUntilClosed(string titleText, string infoText)
        {
            InfoPopupWidget.TitleTextProp.Set(titleText);
            InfoPopupWidget.InfoTextProp.Set(infoText);
            await PopupService.ShowPopupAsync(InfoPopupWidget);
        }
    }
}