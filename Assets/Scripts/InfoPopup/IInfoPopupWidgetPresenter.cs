using System.Threading.Tasks;

namespace InfoPopup
{
    public interface IInfoPopupWidgetPresenter : IWidgetPresenter
    {
        Task ShowAndWaitUntilClosed(string titleText, string infoText);
    }
}