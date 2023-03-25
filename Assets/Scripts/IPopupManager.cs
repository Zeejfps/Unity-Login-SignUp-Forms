using System;
using System.Threading.Tasks;
using YADBF;

namespace Login
{
    public interface IPopupWidget : IWidget
    {
        event Action<IPopupWidget> Closed;
    }
    
    public interface IPopupManager
    {
        ObservableProperty<IPopupWidget> PopupWidgetProp { get; }
        Task ShowPopupAsync(IPopupWidget popupWidget);
    }
}