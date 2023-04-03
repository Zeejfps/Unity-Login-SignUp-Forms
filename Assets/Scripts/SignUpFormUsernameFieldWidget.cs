using Login;
using YADBF;

internal sealed class SignUpFormUsernameFieldWidget : ITextFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);

    public ObservableProperty<string> ErrorTextProp { get; } = new();

    public ITextInputWidget TextInputWidget { get; }

    public SignUpFormUsernameFieldWidget(ISignUpFormController signUpForm)
    {
        TextInputWidget = new SignUpFormUsernameInputWidget(signUpForm);
    }
}