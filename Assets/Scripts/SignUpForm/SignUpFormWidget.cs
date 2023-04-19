using Common.Widgets;
using YADBF;

namespace SignUpForm
{
    internal sealed class SignUpFormWidget : ISignUpFormWidget
    {
        public ObservableProperty<bool> IsVisibleProperty { get; } = new();
        public ITextFieldWidget EmailFieldWidget { get; }
        public ITextFieldWidget UsernameFieldWidget { get; }
        public IPasswordFieldWidget PasswordFieldWidget { get; }
        public IPasswordFieldWidget ConfirmPasswordFieldWidget { get; }
        public IListWidget<IPasswordRequirementWidget> PasswordRequirementsListWidget { get; }
        public IButtonWidget SignUpButtonWidget { get; }

        public SignUpFormWidget(ITextFieldWidget emailFieldWidget) {
            EmailFieldWidget = emailFieldWidget;
            UsernameFieldWidget = new TextFieldWidget();
            PasswordFieldWidget = new PasswordFieldWidget();
            ConfirmPasswordFieldWidget = new PasswordFieldWidget();
            PasswordRequirementsListWidget = new ListWidget<IPasswordRequirementWidget>();
            SignUpButtonWidget = new ButtonWidget();
        }
    }
}