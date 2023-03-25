using Login;

internal sealed class SignUpFormEmailInputWidget : BaseTextInputWidget
{
    public SignUpFormEmailInputWidget(ISignUpForm signUpManager)
    {
        TextProp = signUpManager.EmailProp;
        IsInteractableProperty.Bind(signUpManager.IsLoadingProp, value => !value);
    }

    public void Dispose()
    {
        IsInteractableProperty.Unbind();
    }
}