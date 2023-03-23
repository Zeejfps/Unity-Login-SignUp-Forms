namespace Login
{
    public interface IConfirmSignUpPopupWidget : IPopupWidget
    {
        ITextInputWidget CodeInputWidget { get; }
        IButtonWidget ConfirmButtonWidget { get; }
        IButtonWidget CancelButtonWidget { get; }
    }
}