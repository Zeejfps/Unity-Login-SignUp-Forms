using System;
using Login;
using YADBF;

internal sealed class ClosePopupButtonWidget : IButtonWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsInteractable { get; } = new(true);
    public ObservableProperty<Action> ActionProp { get; } = new();

    private IPopupWidget PopupWidget { get; }
    
    public ClosePopupButtonWidget(IPopupWidget popupWidget)
    {
        PopupWidget = popupWidget;
        ActionProp.Set(ClosePopup);
    }

    private void ClosePopup()
    {
        PopupWidget.IsVisibleProp.Set(false);
    }
}