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
        public IButtonWidget SubmitButtonWidget { get; }

        public SignUpFormWidget(ITextFieldWidget emailFieldWidget, IPasswordFieldWidget passwordFieldWidget,  IButtonWidget submitButtonWidget) {
            EmailFieldWidget = emailFieldWidget;
            UsernameFieldWidget = new TextFieldWidget();
            PasswordFieldWidget = passwordFieldWidget;
            ConfirmPasswordFieldWidget = new PasswordFieldWidget();
            PasswordRequirementsListWidget = new ListWidget<IPasswordRequirementWidget>();
            SubmitButtonWidget = submitButtonWidget;
        }
    }
}