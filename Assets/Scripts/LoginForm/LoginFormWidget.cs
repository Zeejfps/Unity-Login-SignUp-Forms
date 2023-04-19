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

        public LoginFormWidget(
            ITextFieldWidget emailFieldWidget,
            IPasswordFieldWidget passwordFieldWidget,
            IToggleWidget rememberMeToggleWidget,
            IButtonWidget submitButtonWidget
        ) {
            EmailFieldWidget = emailFieldWidget;
            PasswordFieldWidget = passwordFieldWidget;
            RememberMeToggleWidget = rememberMeToggleWidget;
            SubmitButtonWidget = submitButtonWidget;
        }
    }
}