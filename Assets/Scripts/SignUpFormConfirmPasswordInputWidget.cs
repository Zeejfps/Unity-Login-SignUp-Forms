using Login;
using YADBF;

internal class SignUpFormConfirmPasswordInputWidget : ITextInputWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> TextProp { get; }
    public ObservableProperty<bool> IsInteractableProp { get; } = new();
    public ObservableProperty<bool> IsMaskingCharacters { get; } = new(true);
        
    public SignUpFormConfirmPasswordInputWidget(ISignUpForm signUpForm)
    {
        TextProp = signUpForm.ConfirmPasswordProp;
        IsInteractableProp.Bind(signUpForm.IsLoadingProp, value => !value);   
    }
}