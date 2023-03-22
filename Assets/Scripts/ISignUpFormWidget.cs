using YADBF;

public interface ISignUpFormWidget : IWidget
{
    ObservableProperty<ITextInputWidget> EmailInputWidgetProp { get; }
    ObservableProperty<ITextInputWidget> PasswordInputWidgetProp { get; }
    ObservableProperty<ITextInputWidget> ConfirmPasswordInputWidgetProp { get; }
    ObservableProperty<IButtonWidget> SignUpButtonWidgetProp { get; }
}