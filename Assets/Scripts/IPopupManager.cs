using System;
using System.Threading.Tasks;
using Common.Widgets;
using YADBF;

namespace Login
{
    public interface IPopupWidget : IWidget
    {
    }
    
    public interface IPopupManager
    {
        ObservableProperty<IPopupWidget> PopupWidgetProp { get; }

        Task ShowPopupAsync(IPopupWidget popupWidget);
    }
}