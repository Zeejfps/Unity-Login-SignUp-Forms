using YADBF;

public interface ISignUpFormWidget : IWidget
{
    ObservableProperty<ITextInputWidget> EmailInputWidgetProp { get; }
    ObservableProperty<IPasswordInputWidget> PasswordInputWidgetProp { get; }
    ObservableProperty<IPasswordInputWidget> ConfirmPasswordInputWidgetProp { get; }
    IButtonWidget SignUpButtonWidget { get; }
}