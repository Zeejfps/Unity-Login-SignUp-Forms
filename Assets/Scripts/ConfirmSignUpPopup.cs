namespace Login
{
    public interface IConfirmSignUpPopupWidget : IPopup
    {
        ITextInputWidget CodeInputWidget { get; }
        IButtonWidget ConfirmButtonWidget { get; }
        IButtonWidget CancelButtonWidget { get; }
    }
}