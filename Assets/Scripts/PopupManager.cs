using System.Threading.Tasks;
using YADBF;

namespace Login
{
    internal sealed class PopupManager : IPopupManager
    {
        public ObservableProperty<IPopupWidget> PopupWidgetProp { get; } = new();
        
        public Task ShowPopupAsync(IPopupWidget popupWidget)
        {
            var tcs = new TaskCompletionSource<bool>();

            popupWidget.IsVisibleProp.Set(true);
            popupWidget.IsVisibleProp.ValueChanged += (property, wasVisible, isVisible) =>
            {
                if (!isVisible)
                    tcs.SetResult(true);
            };
 
            PopupWidgetProp.Set(popupWidget);

            return tcs.Task;
        }
    }
}