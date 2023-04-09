using Common.Widgets;

namespace Common.Controllers
{
    public interface ITabGroupController
    {
        void LinkTabToContent(ITabWidget tabWidget, IWidget contentWidget);
        void Dispose();
    }
}