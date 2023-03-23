using Login;
using YADBF;

public sealed class ConfirmSignUpPopupWidget : IConfirmSignUpPopupWidget
{
    public ITextInputWidget CodeInputWidget { get; }
    public IButtonWidget ConfirmButtonWidget { get; }
    public IButtonWidget CancelButtonWidget { get; }
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        
    private ISignUpConfirmation SignUpConfirmation { get; }

    public ConfirmSignUpPopupWidget(ISignUpConfirmationFactory signUpConfirmationFactory)
    {
        SignUpConfirmation = signUpConfirmationFactory.Create();
        CodeInputWidget = new ConfirmationCodeInputWidget(SignUpConfirmation);
        ConfirmButtonWidget = new ConfirmSignUpButtonWidget(SignUpConfirmation);
        CancelButtonWidget = new ClosePopupButtonWidget(this);
        
        IsVisibleProp.ValueChanged += IsVisibleProp_OnValueChanged;
        SignUpConfirmation.Confirmed += SignUpConfirmation_OnConfirmed;
    }

    private void SignUpConfirmation_OnConfirmed()
    {
        IsVisibleProp.Set(false);
    }

    private void IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isVisible)
    {
        if (isVisible)
            return;
        
        SignUpConfirmation.Confirmed -= SignUpConfirmation_OnConfirmed;
        SignUpConfirmation.Dispose();
    }
}