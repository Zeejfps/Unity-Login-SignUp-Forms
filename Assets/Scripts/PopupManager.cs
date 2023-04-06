using System.Threading.Tasks;
using YADBF;

namespace Login
{
    internal sealed class PopupManager : IPopupManager
    {
        public ObservableProperty<IPopupWidget> PopupWidgetProp { get; } = new();
        
        public Task ShowPopupAsync(IPopupWidget popupWidget)
        {
            popupWidget.IsVisibleProperty.Set(true);
            PopupWidgetProp.Set(popupWidget);
            return new PopupListener(popupWidget).WaitUntilClosed();
        }

        private sealed class PopupListener
        {
            private TaskCompletionSource<bool> Tcs { get; }
            private IPopupWidget PopupWidget { get; }

            public PopupListener(IPopupWidget popupWidget)
            {
                Tcs = new TaskCompletionSource<bool>();
                PopupWidget = popupWidget;
                popupWidget.IsVisibleProperty.ValueChanged += IsVisibleProperty_OnValueChanged;
            }

            public Task WaitUntilClosed()
            {
                return Tcs.Task;
            }

            private void IsVisibleProperty_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isVisible)
            {
                if (!isVisible)
                {
                    PopupWidget.IsVisibleProperty.ValueChanged -= IsVisibleProperty_OnValueChanged;
                    Tcs.SetResult(true);
                }
            }
        }
    }
}