using YADBF;

namespace Login
{
    internal sealed class BasicSignUpFormWidget : ISignUpFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ObservableProperty<ITextInputWidget> EmailInputWidgetProp { get; } = new();
        public ObservableProperty<ITextInputWidget> PasswordInputWidgetProp { get; } = new();
        public ObservableProperty<ITextInputWidget> ConfirmPasswordInputWidgetProp { get; } = new();

        public BasicSignUpFormWidget()
        {
            EmailInputWidgetProp.Set(new BasicTextInputWidget());
            PasswordInputWidgetProp.Set(new BasicTextInputWidget());
            ConfirmPasswordInputWidgetProp.Set(new BasicTextInputWidget());
        }
    }
}