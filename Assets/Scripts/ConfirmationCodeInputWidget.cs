using Login;
using YADBF;

public sealed class ConfirmationCodeInputWidget : ITextInputWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> TextProp { get; }
    public ObservableProperty<bool> IsInteractableProp { get; } = new();
    public ObservableProperty<bool> IsMaskingCharacters { get; } = new();

    private ISignUpConfirmationManager SignUpConfirmationManager { get; }
    
    public ConfirmationCodeInputWidget(ISignUpConfirmationManager signUpConfirmationManager)
    {
        SignUpConfirmationManager = signUpConfirmationManager;
        SignUpConfirmationManager.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;

        TextProp = signUpConfirmationManager.ConfirmationCodeTextProp;
        
        UpdateState();
    }

    private void IsLoadingProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
    {
        UpdateState();
    }

    private void UpdateState()
    {
        var isLoading = SignUpConfirmationManager.IsLoadingProp.Value;
        IsInteractableProp.Set(!isLoading);
    }
}