using System;
using System.Threading;
using System.Threading.Tasks;
using Login;
using UnityEngine;
using YADBF;

namespace SignUpConfirmationForm
{
    public sealed class SignUpConfirmationPopupWidgetController : ISignUpConfirmationPopupWidgetController
    {
        public event Action<ISignUpConfirmationPopupWidgetController> FormSubmitted;


        public string ConfirmationCode => ConfirmationCodeTextInputWidget.TextProp.Value;

        private ISignUpConfirmationPopupWidget SignUpConfirmationPopupWidget { get; }
        private ITextInputWidget ConfirmationCodeTextInputWidget => SignUpConfirmationPopupWidget.CodeInputWidget;
        private IButtonWidget ConfirmButtonWidget => SignUpConfirmationPopupWidget.ConfirmButtonWidget;
        private IButtonWidget CancelButtonWidget => SignUpConfirmationPopupWidget.CancelButtonWidget;
        
        private CancellationTokenSource m_CancellationTokenSource;

        public SignUpConfirmationPopupWidgetController(ISignUpConfirmationPopupWidget signUpConfirmationPopupWidget)
        {
            SignUpConfirmationPopupWidget = signUpConfirmationPopupWidget;

            CancelButtonWidget.ActionProp.Set(Cancel);
            ConfirmButtonWidget.ActionProp.Set(Submit);
            
            ConfirmationCodeTextInputWidget.IsInteractableProperty.Set(true);
            CancelButtonWidget.IsInteractableProperty.Set(true);
            
            ConfirmationCodeTextInputWidget.TextProp.ValueChanged += ConfirmationCodeTextInputWidget_TextProp_OnValueChanged;
            
            UpdateConfirmationButtonInteractionState();
        }

        public void Dispose()
        {
            ConfirmationCodeTextInputWidget.TextProp.ValueChanged -= ConfirmationCodeTextInputWidget_TextProp_OnValueChanged;
        }

        public bool ProcessInputEvent(InputEvent inputEvent)
        {
            return false;
        }

        private void Cancel()
        {
            m_CancellationTokenSource?.Cancel();
            SignUpConfirmationPopupWidget.IsVisibleProp.Set(false);
        }

        private void ConfirmationCodeTextInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateConfirmationButtonInteractionState();
        }

        private void UpdateConfirmationButtonInteractionState()
        {
            var confirmationCode = ConfirmationCode;
            if (string.IsNullOrEmpty(confirmationCode))
                ConfirmButtonWidget.IsInteractableProperty.Set(false);
            else
                ConfirmButtonWidget.IsInteractableProperty.Set(true);
        }

        private async void Submit()
        {
            try
            {
                m_CancellationTokenSource = new CancellationTokenSource();
                //IsLoadingProp.Set(true);
                await Task.Delay(3000, m_CancellationTokenSource.Token);
                FormSubmitted?.Invoke(this);
                SignUpConfirmationPopupWidget.IsVisibleProp.Set(false);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                //IsLoadingProp.Set(false);
            }
        }
    }
}