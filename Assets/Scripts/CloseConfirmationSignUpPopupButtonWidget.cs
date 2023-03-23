using System;
using Login;
using YADBF;

public sealed class CloseConfirmationSignUpPopupButtonWidget : IButtonWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsInteractable { get; } = new(true);
    public ObservableProperty<Action> ActionProp { get; } = new();

    private ISignUpConfirmation SignUpConfirmation { get; }
    private IPopupWidget PopupWidget { get; }

    public CloseConfirmationSignUpPopupButtonWidget(ISignUpConfirmation signUpConfirmation, IPopupWidget popupWidget)
    {
        SignUpConfirmation = signUpConfirmation;
        PopupWidget = popupWidget;
        ActionProp.Set(ClosePopup);
    }

    private void ClosePopup()
    {
        SignUpConfirmation.Dispose();
        PopupWidget.IsVisibleProp.Set(false);
    }
}