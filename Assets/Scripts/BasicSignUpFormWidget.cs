using System;
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
        
        private ISignUpManager SignUpManager { get; }

        private ITextInputWidget PasswordTextInputWidget => PasswordInputWidget.TextInputWidget;
        
        public BasicSignUpFormWidget(ISignUpManager signUpManager)
        {
            SignUpManager = signUpManager;

            EmailInputWidget = new SignUpEmailInputWidget(signUpManager);
            PasswordInputWidget = new StringPropPasswordInputWidget(signUpManager.PasswordProp);
            ConfirmPasswordInputWidget = new StringPropPasswordInputWidget(signUpManager.ConfirmPasswordProp);
            SignUpButtonWidget = new SignUpFormSignUpButton(signUpManager);
            
            EmailInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
            PasswordTextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
            ConfirmPasswordInputWidget.TextInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
        }

        private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateButtonState();
        }

        private async void SignUp()
        {
            var emailInputWidget = EmailInputWidget;
            var passwordInputWidget = PasswordInputWidget.TextInputWidget;
            var confirmPasswordInputWidget = ConfirmPasswordInputWidget.TextInputWidget;
            var signUpButtonWidget = SignUpButtonWidget;
            
            try
            {
                emailInputWidget.IsInteractableProp.Set(false);
                passwordInputWidget.IsInteractableProp.Set(false);
                confirmPasswordInputWidget.IsInteractableProp.Set(false);
                signUpButtonWidget.IsInteractable.Set(false);
                
                var email = EmailInputWidget.TextProp.Value;
                var password = PasswordInputWidget.TextInputWidget.TextProp.Value;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                emailInputWidget.IsInteractableProp.Set(true);
                passwordInputWidget.IsInteractableProp.Set(true);
                confirmPasswordInputWidget.IsInteractableProp.Set(true);
                UpdateButtonState();   
            }
        }
        
        private void UpdateButtonState()
        {
            var signUpButtonWidget = SignUpButtonWidget;
            var allFieldsValid = ValidateFields();
            if (allFieldsValid)
                signUpButtonWidget.IsInteractable.Set(true);
            else
                signUpButtonWidget.IsInteractable.Set(false);
        }
        
        private bool ValidateFields()
        {
            var email = EmailInputWidget.TextProp.Value;
            var password = PasswordInputWidget.TextInputWidget.TextProp.Value;
            var confirmPassword = ConfirmPasswordInputWidget.TextInputWidget.TextProp.Value;
            
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (string.IsNullOrWhiteSpace(confirmPassword))
                return false;
            
            return true;
        }
    }

    internal abstract class BaseTextInputWidget : ITextInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; } = new();
        public ObservableProperty<bool> IsInteractableProp { get; } = new();
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();
    } 
    
    internal sealed class SignUpEmailInputWidget : BaseTextInputWidget
    {
        public SignUpEmailInputWidget(ISignUpManager signUpManager)
        {
            IsInteractableProp.Bind(signUpManager.IsLoadingProp, value => !value);
            TextProp.Bind(signUpManager.EmailProp);
        }

        public void Dispose()
        {
            IsInteractableProp.Unbind();
            TextProp.Unbind();
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
    
    internal sealed class SignUpFormSignUpButton : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsInteractable { get; } = new();
        public ObservableProperty<Action> ActionProp { get; } = new();

        private ISignUpManager SignUpManager { get; }
        
        public SignUpFormSignUpButton(ISignUpManager signUpManager)
        {
            SignUpManager = signUpManager;
            signUpManager.SignUpActionProp.ValueChanged += SignUpActionProp_OnValueChanged;
            UpdateState();
        }

        private void SignUpActionProp_OnValueChanged(ObservableProperty<Action> property, Action prevvalue, Action currvalue)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            var signUpAction = SignUpManager.SignUpActionProp.Value;
            ActionProp.Set(signUpAction);
            IsInteractable.Set(signUpAction != null);
        }
    } 
}