using Login;
using YADBF;

public sealed class ConfirmationCodeInputWidget : ITextInputWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> TextProp { get; }
    public ObservableProperty<bool> IsInteractableProp { get; } = new();
    public ObservableProperty<bool> IsMaskingCharacters { get; } = new();

    private ISignUpConfirmationFlow SignUpConfirmationFlowManager { get; }
    
    public ConfirmationCodeInputWidget(ISignUpConfirmationFlow signUpConfirmationFlowManager)
    {
        SignUpConfirmationFlowManager = signUpConfirmationFlowManager;
        SignUpConfirmationFlowManager.IsLoadingProp.ValueChanged += IsLoadingProp_OnValueChanged;

        TextProp = signUpConfirmationFlowManager.ConfirmationCodeTextProp;
        
        UpdateState();
    }

    private void IsLoadingProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
    {
        UpdateState();
    }

    private void UpdateState()
    {
        var isLoading = SignUpConfirmationFlowManager.IsLoadingProp.Value;
        IsInteractableProp.Set(!isLoading);
    }
}