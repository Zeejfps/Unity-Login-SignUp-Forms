using YADBF;

public interface ISignUpFormWidget : IWidget
{
    ObservableProperty<ITextInputWidget> EmailInputWidgetProp { get; }
    ObservableProperty<IPasswordInputWidget> PasswordInputWidgetProp { get; }
    ObservableProperty<IPasswordInputWidget> ConfirmPasswordInputWidgetProp { get; }
    ObservableProperty<IButtonWidget> SignUpButtonWidgetProp { get; }
}