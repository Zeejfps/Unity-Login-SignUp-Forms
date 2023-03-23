using Login;
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