using Login;
using Widgets;
using YADBF;

namespace SignUpConfirmationForm
{
    public sealed class SignUpConfirmationPopupWidget : ISignUpConfirmationPopupWidget
    {
        public ITextInputWidget CodeInputWidget { get; }
        public IButtonWidget ConfirmButtonWidget { get; }
        public IButtonWidget CancelButtonWidget { get; }
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        
        public SignUpConfirmationPopupWidget()
        {
            CodeInputWidget = new TextInputWidget();
            ConfirmButtonWidget = new ButtonWidget();
            CancelButtonWidget = new ButtonWidget();
        }
    }
}