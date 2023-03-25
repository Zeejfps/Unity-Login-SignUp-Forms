using Login;
using YADBF;

public sealed class ConfirmationCodeInputWidget : ITextInputWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> TextProp { get; }
    public ObservableProperty<bool> IsInteractableProp { get; } = new();
    public ObservableProperty<bool> IsMaskingCharacters { get; } = new();

    private ISignUpConfirmationForm SignUpConfirmationFormManager { get; }
    
    public ConfirmationCodeInputWidget(ISignUpConfirmationForm signUpConfirmationFormManager)
    {
        SignUpConfirmationFormManager = signUpConfirmationFormManager;
        SignUpConfirmationFormManager.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;

        TextProp = signUpConfirmationFormManager.ConfirmationCodeTextProp;
        
        UpdateState();
    }

    private void IsLoadingProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
    {
        UpdateState();
    }

    private void UpdateState()
    {
        var isLoading = SignUpConfirmationFormManager.IsLoadingProp.Value;
        IsInteractableProp.Set(!isLoading);
    }
}