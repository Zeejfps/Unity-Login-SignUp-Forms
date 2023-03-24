using System;
using Login;
using YADBF;

public sealed class ConfirmSignUpPopupWidget : IConfirmSignUpPopupWidget
{
    public event Action<IPopupWidget> Closed;

    public ITextInputWidget CodeInputWidget { get; }
    public IButtonWidget ConfirmButtonWidget { get; }
    public IButtonWidget CancelButtonWidget { get; }
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        
    private ISignUpConfirmationFlow SignUpConfirmationFlow { get; }

    public ConfirmSignUpPopupWidget(ISignUpConfirmationFlow signUpConfirmationFlow)
    {
        SignUpConfirmationFlow = signUpConfirmationFlow;
        CodeInputWidget = new ConfirmationCodeInputWidget(SignUpConfirmationFlow);
        ConfirmButtonWidget = new ConfirmSignUpButtonWidget(SignUpConfirmationFlow);
        CancelButtonWidget = new ClosePopupButtonWidget(this);
        
        IsVisibleProp.ValueChanged += IsVisibleProp_OnValueChanged;
        SignUpConfirmationFlow.Completed += SignUpConfirmation_OnConfirmed;
    }

    private void SignUpConfirmation_OnConfirmed(ISignUpConfirmationFlow flow)
    {
        flow.Completed -= SignUpConfirmation_OnConfirmed;
        IsVisibleProp.Set(false);
    }

    private void IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isVisible)
    {
        if (isVisible)
            return;
        
        SignUpConfirmationFlow.Completed -= SignUpConfirmation_OnConfirmed;
        SignUpConfirmationFlow.Cancel();
        Closed?.Invoke(this);
    }
}