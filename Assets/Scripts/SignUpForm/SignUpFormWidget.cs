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

        public SignUpFormWidget(
            ITextFieldWidget emailFieldWidget,
            ITextFieldWidget usernameFieldWidget,
            IPasswordFieldWidget passwordFieldWidget,  
            IPasswordFieldWidget confirmPasswordFieldWidget,
            IButtonWidget submitButtonWidget
        ) {
            EmailFieldWidget = emailFieldWidget;
            UsernameFieldWidget = usernameFieldWidget;
            PasswordFieldWidget = passwordFieldWidget;
            ConfirmPasswordFieldWidget = confirmPasswordFieldWidget;
            PasswordRequirementsListWidget = new ListWidget<IPasswordRequirementWidget>();
            SubmitButtonWidget = submitButtonWidget;
        }
    }
}