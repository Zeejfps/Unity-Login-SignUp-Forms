using System;
using YADBF;

namespace Login
{
    public interface IInfoPopupWidget
    {
        ObservableProperty<string> TitleTextProp { get; }
        ObservableProperty<string> InfoTextProp { get; }
        ObservableProperty<Action> OkActionProp { get; }
    }
}