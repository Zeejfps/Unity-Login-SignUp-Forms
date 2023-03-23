using Login;
using YADBF;

public sealed class ConfirmSignUpPopupWidget : IConfirmSignUpPopupWidget
{
    public ITextInputWidget CodeInputWidget { get; }
    public IButtonWidget ConfirmButtonWidget { get; }
    public IButtonWidget CancelButtonWidget { get; }
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        
    private ISignUpConfirmation SignUpConfirmation { get; }

    public ConfirmSignUpPopupWidget(ISignUpConfirmation signUpConfirmation)
    {
        SignUpConfirmation = signUpConfirmation;
        CodeInputWidget = new ConfirmationCodeInputWidget(signUpConfirmation);
        ConfirmButtonWidget = new ConfirmSignUpButtonWidget(signUpConfirmation);
        CancelButtonWidget = new CloseConfirmationSignUpPopupButtonWidget(signUpConfirmation, this);
        
        IsVisibleProp.ValueChanged += IsVisibleProp_OnValueChanged;
        signUpConfirmation.Confirmed += SignUpConfirmation_OnConfirmed;
    }

    private void SignUpConfirmation_OnConfirmed()
    {
        IsVisibleProp.Set(false);
    }

    private void IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
    {
        SignUpConfirmation.Confirmed -= SignUpConfirmation_OnConfirmed;
    }
}