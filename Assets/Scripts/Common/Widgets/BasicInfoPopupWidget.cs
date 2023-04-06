using System;
using Login;
using YADBF;

namespace Common.Widgets
{
    internal sealed class BasicInfoPopupWidget : IInfoPopupWidget
    {
        public event Action<IPopupWidget> Closed;

        public ObservableProperty<string> TitleTextProp { get; } = new();
        public ObservableProperty<string> InfoTextProp { get; } = new();
        public ObservableProperty<Action> OkActionProp { get; } = new();
        public ObservableProperty<bool> IsVisibleProperty { get; } = new(true);

        public BasicInfoPopupWidget()
        {
            OkActionProp.Set(Close);
            IsVisibleProperty.ValueChanged += IsVisibleProp_OnValueChanged;
        }

        private void Close()
        {
            IsVisibleProperty.Set(false);
        }

        private void IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
        {
            if (currvalue)
                return;
            
            Closed?.Invoke(this);
        }
    }
}