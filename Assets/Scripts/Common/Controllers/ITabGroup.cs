using System;
using Common.Widgets;

namespace Common.Controllers
{
    public interface ITabGroupController : IDisposable
    {
        void LinkTabToContent(ITabWidget tabWidget, IWidget contentWidget);
    }
}