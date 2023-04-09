using Common.Widgets;
using Login;
using YADBF;

namespace InfoPopup
{
    public interface IInfoPopupWidget : IPopupWidget
    {
        ObservableProperty<string> TitleTextProp { get; }
        ObservableProperty<string> InfoTextProp { get; }
        IButtonWidget OkButtonWidget { get; }
    }
}