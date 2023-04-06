using Common.Widgets;
using YADBF;

namespace LoginForm
{
    internal sealed class LoginFormWidget : ILoginFormWidget
    {
        public ObservableProperty<bool> IsVisibleProperty { get; } = new();
        public ITextFieldWidget EmailFieldWidget { get; }
        public IPasswordFieldWidget PasswordFieldWidget { get; }
        public IButtonWidget SubmitButtonWidget { get; }
        public IToggleWidget RememberMeToggleWidget { get; }

        public LoginFormWidget() {
            EmailFieldWidget = new TextFieldWidget();
            PasswordFieldWidget = new PasswordFieldWidget();
            SubmitButtonWidget = new ButtonWidget();
            RememberMeToggleWidget = new ToggleWidget();
        }
    }
}