using Common.Widgets;
using Login;

namespace SignUpConfirmationForm
{
    public interface ISignUpConfirmationPopupWidget : IPopupWidget
    {
        ITextInputWidget CodeInputWidget { get; }
        IButtonWidget ConfirmButtonWidget { get; }
        IButtonWidget CancelButtonWidget { get; }
    }
}