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
        public string ConfirmationCode => ConfirmationCodeTextInputWidget.TextProp.Value;

        private ISignUpConfirmationPopupWidget SignUpConfirmationPopupWidget { get; }
        private ITextInputWidget ConfirmationCodeTextInputWidget => SignUpConfirmationPopupWidget.CodeInputWidget;
        private IButtonWidget ConfirmButtonWidget => SignUpConfirmationPopupWidget.ConfirmButtonWidget;
        private IButtonWidget CancelButtonWidget => SignUpConfirmationPopupWidget.CancelButtonWidget;

        private bool m_IsLoading;
        private bool IsLoading
        {
            get => m_IsLoading;
            set
            {
                m_IsLoading = value;
                CancelButtonWidget.IsInteractableProperty.Set(!m_IsLoading);
                ConfirmationCodeTextInputWidget.IsInteractableProperty.Set(!m_IsLoading);
                UpdateConfirmationButtonInteractionState();
            }
        }
        
        private CancellationTokenSource m_CancellationTokenSource;

        public SignUpConfirmationPopupWidgetController(ISignUpConfirmationPopupWidget signUpConfirmationPopupWidget)
        {
            SignUpConfirmationPopupWidget = signUpConfirmationPopupWidget;

            CancelButtonWidget.ActionProp.Set(Cancel);
            ConfirmButtonWidget.ActionProp.Set(Submit);

            CancelButtonWidget.IsInteractableProperty.Set(true);
            ConfirmationCodeTextInputWidget.IsInteractableProperty.Set(true);
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
            if (string.IsNullOrEmpty(confirmationCode) || IsLoading)
                ConfirmButtonWidget.IsInteractableProperty.Set(false);
            else
                ConfirmButtonWidget.IsInteractableProperty.Set(true);
        }

        private async void Submit()
        {
            try
            {
                m_CancellationTokenSource?.Cancel();
                m_CancellationTokenSource = new CancellationTokenSource();
                IsLoading = true;
                await Task.Delay(3000, m_CancellationTokenSource.Token);
                SignUpConfirmationPopupWidget.IsVisibleProp.Set(false);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}