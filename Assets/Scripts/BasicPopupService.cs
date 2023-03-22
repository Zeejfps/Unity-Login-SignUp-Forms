using System;
using System.Threading.Tasks;
using YADBF;

namespace Login
{
    internal sealed class BasicPopupService : IPopupService
    {
        public ObservableProperty<IInfoPopupWidget> InfoPopupWidgetProp { get; } = new();
        public Task ShowInfoPopupAsync(string titleText, string infoText)
        {
            var tcs = new TaskCompletionSource<bool>();
            var popup = new BasicInfoPopupWidget();
            popup.TitleTextProp.Set(titleText);
            popup.InfoTextProp.Set(infoText);
            popup.OkActionProp.Set(() =>
            {
                InfoPopupWidgetProp.Set(null);
                tcs.SetResult(true);
            });
            InfoPopupWidgetProp.Set(popup);
            return tcs.Task;
        }
    }
    
    internal sealed class BasicInfoPopupWidget : IInfoPopupWidget
    {
        public ObservableProperty<string> TitleTextProp { get; } = new();
        public ObservableProperty<string> InfoTextProp { get; } = new();
        public ObservableProperty<Action> OkActionProp { get; } = new();
    }
}