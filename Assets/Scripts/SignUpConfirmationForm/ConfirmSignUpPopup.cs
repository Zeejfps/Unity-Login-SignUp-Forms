namespace Login
{
    public interface ISignUpConfirmationPopupWidget : IPopupWidget
    {
        ITextInputWidget CodeInputWidget { get; }
        IButtonWidget ConfirmButtonWidget { get; }
        IButtonWidget CancelButtonWidget { get; }
    }
}