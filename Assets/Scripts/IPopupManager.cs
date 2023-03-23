using System.Threading.Tasks;
using YADBF;

namespace Login
{
    public interface IPopup : IWidget
    {
        
    }
    
    public interface IPopupManager
    {
        ObservableProperty<IInfoPopupWidget> InfoPopupWidgetProp { get; }
        Task ShowInfoPopupAsync(string titleText, string infoText);

        Task ShowPopupAsync(IPopup popup);
    }
}