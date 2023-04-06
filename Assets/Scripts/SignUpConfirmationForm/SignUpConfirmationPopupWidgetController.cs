using System;
using System.Threading;
using UnityEngine;
using YADBF;

namespace SignUpConfirmationForm
{
    public sealed class SignUpConfirmationPopupWidgetController : ISignUpConfirmationPopupWidgetController
    {
        public event Action Confirmed;
        public event Action Canceled;
        
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
                ConfirmButtonWidget.IsLoadingProp.Set(m_IsLoading);
                CancelButtonWidget.IsInteractableProperty.Set(!m_IsLoading);
                ConfirmationCodeTextInputWidget.IsInteractableProperty.Set(!m_IsLoading);
                UpdateConfirmationButtonInteractionState();
            }
        }
        
        private ISignUpConfirmationService SignUpConfirmationService { get; }
        
        private CancellationTokenSource m_CancellationTokenSource;

        public SignUpConfirmationPopupWidgetController(ISignUpConfirmationService signUpConfirmationService, ISignUpConfirmationPopupWidget signUpConfirmationPopupWidget)
        {
            SignUpConfirmationService = signUpConfirmationService;
            SignUpConfirmationPopupWidget = signUpConfirmationPopupWidget;

            CancelButtonWidget.ActionProp.Set(Cancel);
            ConfirmButtonWidget.ActionProp.Set(Confirm);

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
            SignUpConfirmationPopupWidget.IsVisibleProp.Set(false);
            m_CancellationTokenSource?.Cancel();
            Canceled?.Invoke();
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

        private async void Confirm()
        {
            try
            {
                m_CancellationTokenSource?.Cancel();
                m_CancellationTokenSource = new CancellationTokenSource();
                IsLoading = true;
                await SignUpConfirmationService.ConfirmSignUp(ConfirmationCode, m_CancellationTokenSource.Token);
                SignUpConfirmationPopupWidget.IsVisibleProp.Set(false);
                Confirmed?.Invoke();
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