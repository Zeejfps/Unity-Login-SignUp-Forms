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
        
    private ISignUpConfirmationForm SignUpConfirmationForm { get; }

    public ConfirmSignUpPopupWidget(ISignUpConfirmationForm signUpConfirmationForm)
    {
        SignUpConfirmationForm = signUpConfirmationForm;
        CodeInputWidget = new ConfirmationCodeInputWidget(SignUpConfirmationForm);
        ConfirmButtonWidget = new ConfirmSignUpButtonWidget(SignUpConfirmationForm);
        CancelButtonWidget = new ClosePopupButtonWidget(this);
        
        IsVisibleProp.ValueChanged += IsVisibleProp_OnValueChanged;
        SignUpConfirmationForm.Submitted += SignUpConfirmation_OnConfirmed;
    }

    private void SignUpConfirmation_OnConfirmed(ISignUpConfirmationForm form)
    {
        form.Submitted -= SignUpConfirmation_OnConfirmed;
        IsVisibleProp.Set(false);
    }

    private void IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isVisible)
    {
        if (isVisible)
            return;
        
        SignUpConfirmationForm.Submitted -= SignUpConfirmation_OnConfirmed;
        SignUpConfirmationForm.Cancel();
        Closed?.Invoke(this);
    }
}