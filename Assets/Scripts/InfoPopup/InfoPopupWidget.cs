using Login;
using YADBF;

namespace Common.Widgets
{
    public sealed class InfoPopupWidget : IInfoPopupWidget
    {
        public ObservableProperty<string> TitleTextProp { get; } = new();
        public ObservableProperty<string> InfoTextProp { get; } = new();
        public ObservableProperty<bool> IsVisibleProperty { get; } = new(true);
        public IButtonWidget OkButtonWidget { get; } = new ButtonWidget();
    }
}