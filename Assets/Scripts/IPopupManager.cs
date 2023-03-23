using System.Threading.Tasks;
using YADBF;

namespace Login
{
    public interface IPopupManager
    {
        ObservableProperty<IInfoPopupWidget> InfoPopupWidgetProp { get; }
        Task ShowInfoPopupAsync(string titleText, string infoText);
    }
}