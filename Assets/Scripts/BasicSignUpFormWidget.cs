using UnityEngine;
using YADBF;

namespace Login
{
    internal sealed class BasicSignUpFormWidget : ISignUpFormWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new();
        public ITextInputWidget EmailInputWidget { get; }
        public IPasswordInputWidget PasswordInputWidget { get; }
        public IPasswordInputWidget ConfirmPasswordInputWidget { get; }
        public IButtonWidget SignUpButtonWidget { get; }
        
        public BasicSignUpFormWidget(ISignUpManager signUpManager)
        {
            EmailInputWidget = new SignUpFormEmailInputWidget(signUpManager);
            PasswordInputWidget = new SignUpFormPasswordInputWidget(signUpManager);
            ConfirmPasswordInputWidget = new StringPropPasswordInputWidget(signUpManager.ConfirmPasswordProp);
            SignUpButtonWidget = new SignUpFormSignUpButton(signUpManager); 
        }
    }

    internal class StringPropTextInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; } 
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();

        public StringPropTextInputWidget(ObservableProperty<string> textProp)
        {
            TextProp = textProp;
        }
    }

    internal sealed class StringPropPasswordInputWidget : IPasswordInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ITextInputWidget TextInputWidget { get; }
        public IToggleWidget ShowPasswordToggleWidget { get; }

        public StringPropPasswordInputWidget(ObservableProperty<string> textProp)
        {
            TextInputWidget = new StringPropTextInputWidget(textProp);
            ShowPasswordToggleWidget = new ShowPasswordToggleWidget(TextInputWidget);
        }
    }
}