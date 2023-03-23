using System;
using Login;
using UnityEditor.Purchasing;
using YADBF;

public sealed class ConfirmSignUpPopupWidget : IConfirmSignUpPopupWidget
{
    public ITextInputWidget CodeInputWidget { get; }
    public IButtonWidget ConfirmButtonWidget { get; }
    public IButtonWidget CancelButtonWidget { get; }
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        
    private ISignUpConfirmationManager SignUpManager { get; }

    public ConfirmSignUpPopupWidget(ISignUpConfirmationManager signUpManager)
    {
        SignUpManager = signUpManager;
        CodeInputWidget = new ConfirmationCodeInputWidget(signUpManager);
        ConfirmButtonWidget = new ConfirmSignUpButtonWidget(signUpManager);
        CancelButtonWidget = new ClosePopupButtonWidget(this);
    }
}

public sealed class CloseConfirmationSignUpPopupButtonWidget : IButtonWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsInteractable { get; } = new(true);
    public ObservableProperty<Action> ActionProp { get; } = new();

    private ISignUpConfirmationManager SignUpConfirmationManager { get; }
    private IPopupWidget PopupWidget { get; }

    public CloseConfirmationSignUpPopupButtonWidget(ISignUpConfirmationManager signUpConfirmationManager, IPopupWidget popupWidget)
    {
        SignUpConfirmationManager = signUpConfirmationManager;
        PopupWidget = popupWidget;
        ActionProp.Set(ClosePopup);
    }

    private void ClosePopup()
    {
        //SignUpConfirmationManager.CancelSignUp();
        PopupWidget.IsVisibleProp.Set(false);
    }
}