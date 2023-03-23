using System;
using YADBF;

namespace Login
{
    public interface IInfoPopupWidget : IPopupWidget
    {
        ObservableProperty<string> TitleTextProp { get; }
        ObservableProperty<string> InfoTextProp { get; }
        ObservableProperty<Action> OkActionProp { get; }
    }
}