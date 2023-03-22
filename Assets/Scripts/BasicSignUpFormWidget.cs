using System;
using YADBF;

namespace Login
{
    internal sealed class BasicSignUpFormWidget : ISignUpFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ObservableProperty<ITextInputWidget> EmailInputWidgetProp { get; } = new();
        public ObservableProperty<ITextInputWidget> PasswordInputWidgetProp { get; } = new();
        public ObservableProperty<ITextInputWidget> ConfirmPasswordInputWidgetProp { get; } = new();
        public ObservableProperty<IButtonWidget> SignUpButtonWidgetProp { get; } = new();
        
        public BasicSignUpFormWidget()
        {
            EmailInputWidgetProp.Set(new BasicTextInputWidget());
            PasswordInputWidgetProp.Set(new BasicTextInputWidget());
            ConfirmPasswordInputWidgetProp.Set(new BasicTextInputWidget());
            SignUpButtonWidgetProp.Set(new SignUpFormSignUpButtonWidget(this));
        }
    }

    internal sealed class SignUpFormSignUpButtonWidget : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractable { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();

        private ISignUpFormWidget SignUpFormWidget { get; }
        
        public SignUpFormSignUpButtonWidget(ISignUpFormWidget signUpFormWidget)
        {
            SignUpFormWidget = signUpFormWidget;
            signUpFormWidget.EmailInputWidgetProp.Value.TextProp.ValueChanged += TextProp_OnValueChanged;
            signUpFormWidget.PasswordInputWidgetProp.Value.TextProp.ValueChanged += TextProp_OnValueChanged;
            signUpFormWidget.ConfirmPasswordInputWidgetProp.Value.TextProp.ValueChanged += TextProp_OnValueChanged;
            UpdateIsInteractableState();
        }

        private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateIsInteractableState();
        }

        private void UpdateIsInteractableState()
        {
            var email = SignUpFormWidget.EmailInputWidgetProp.Value.TextProp.Value;
            var password = SignUpFormWidget.PasswordInputWidgetProp.Value.TextProp.Value;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                IsInteractable.Set(false);
            else
                IsInteractable.Set(true);
        }
    }
}