namespace Login
{
    internal sealed class SignUpFormEmailInputWidget : BaseTextInputWidget
    {
        public SignUpFormEmailInputWidget(ISignUpManager signUpManager)
        {
            IsInteractableProp.Bind(signUpManager.IsLoadingProp, value => !value);
            TextProp.Bind(signUpManager.EmailProp);
        }

        public void Dispose()
        {
            IsInteractableProp.Unbind();
            TextProp.Unbind();
        }
    }
}