using System;
using YADBF;

namespace Login
{
    internal sealed class BasicInfoPopupWidget : IInfoPopupWidget
    {
        public ObservableProperty<string> TitleTextProp { get; } = new();
        public ObservableProperty<string> InfoTextProp { get; } = new();
        public ObservableProperty<Action> OkActionProp { get; } = new();
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    }
}