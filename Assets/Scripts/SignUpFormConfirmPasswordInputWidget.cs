namespace Login
{
    internal class SignUpFormConfirmPasswordInputWidget : BaseTextInputWidget
    {
        public SignUpFormConfirmPasswordInputWidget(ISignUpManager signUpManager)
        {
            TextProp.Bind(signUpManager.ConfirmPasswordProp);
            IsInteractableProp.Bind(signUpManager.IsLoadingProp, value => !value);   
        }
    }
}