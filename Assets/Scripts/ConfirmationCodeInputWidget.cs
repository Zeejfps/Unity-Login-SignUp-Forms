using Login;
using YADBF;

public sealed class ConfirmationCodeInputWidget : BaseTextInputWidget
{
    private ISignUpConfirmationForm SignUpConfirmationForm { get; }
    
    public ConfirmationCodeInputWidget(ISignUpConfirmationForm signUpConfirmationForm)
    {
        SignUpConfirmationForm = signUpConfirmationForm;
        SignUpConfirmationForm.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;

        TextProp = signUpConfirmationForm.ConfirmationCodeTextProp;
        
        UpdateState();
    }

    private void IsLoadingProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
    {
        UpdateState();
    }

    private void UpdateState()
    {
        var isLoading = SignUpConfirmationForm.IsLoadingProp.Value;
        IsInteractableProperty.Set(!isLoading);
    }
}