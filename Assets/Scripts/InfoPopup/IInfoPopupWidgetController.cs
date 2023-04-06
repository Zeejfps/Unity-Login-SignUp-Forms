using System.Threading.Tasks;

namespace Common.Widgets
{
    public interface IInfoPopupWidgetController : IWidgetController
    {
        Task ShowAndWaitUntilClosed(string titleText, string infoText);
    }
}