namespace Login
{
    internal sealed class SignUpFormPasswordInputWidget : BaseTextInputWidget
    {
        public SignUpFormPasswordInputWidget(ISignUpManager signUpManager)
        {
            TextProp.Bind(signUpManager.PasswordProp);
            IsInteractableProp.Bind(signUpManager.IsLoadingProp, value => !value);
        }
    }
}