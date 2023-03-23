using System.Threading.Tasks;
using YADBF;

namespace Login
{
    internal sealed class PopupManager : IPopupManager
    {
        public ObservableProperty<IPopupWidget> PopupWidgetProp { get; } = new();
        
        public Task ShowInfoPopupAsync(string titleText, string infoText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var popup = new BasicInfoPopupWidget();
            popup.TitleTextProp.Set(titleText);
            popup.InfoTextProp.Set(infoText);
            popup.OkActionProp.Set(() =>
            {
                PopupWidgetProp.Set(null);
                tcs.SetResult(true);
            });
            PopupWidgetProp.Set(popup);
            return tcs.Task;
        }
    }
}