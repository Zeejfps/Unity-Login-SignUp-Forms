using Login;
using YADBF;

internal sealed class SignUpFormEmailFieldWidget : ITextFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> ErrorTextProp { get; } = new();
    public ITextInputWidget TextInputWidget { get; }

    public SignUpFormEmailFieldWidget(ISignUpForm signUpManager)
    {
        TextInputWidget = new SignUpFormEmailInputWidget(signUpManager);
    }
}