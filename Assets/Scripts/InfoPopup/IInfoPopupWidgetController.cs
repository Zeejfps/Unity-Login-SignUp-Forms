using System.Threading.Tasks;

namespace Common.Widgets
{
    public interface IInfoPopupWidgetController : IWidgetPresenter
    {
        Task ShowAndWaitUntilClosed(string titleText, string infoText);
    }
}