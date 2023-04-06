using System;
using Common.Widgets;
using YADBF;

namespace Login
{
    public interface IInfoPopupWidget : IPopupWidget
    {
        ObservableProperty<string> TitleTextProp { get; }
        ObservableProperty<string> InfoTextProp { get; }
        IButtonWidget OkButtonWidget { get; }
    }
}