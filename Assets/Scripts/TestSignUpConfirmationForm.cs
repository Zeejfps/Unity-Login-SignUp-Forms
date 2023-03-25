using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using YADBF;

namespace Login
{
    public sealed class TestSignUpConfirmationForm : ISignUpConfirmationForm
    {
        public event Action<ISignUpConfirmationForm> Submitted;
        
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<Action> ConfirmActionProp { get; } = new();
        public ObservableProperty<string> ConfirmationCodeTextProp { get; } = new();

        private CancellationTokenSource m_CancellationTokenSource;
        
        public void Cancel()
        {
            m_CancellationTokenSource?.Cancel();
        }

        public TestSignUpConfirmationForm()
        {
            ConfirmationCodeTextProp.ValueChanged += ConfirmationCodeTextProp_OnValueChanged;
        }

        private void ConfirmationCodeTextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
        {
            UpdateConfirmationActionState();
        }

        private void UpdateConfirmationActionState()
        {
            var confirmationCode = ConfirmationCodeTextProp.Value;
            if (string.IsNullOrEmpty(confirmationCode))
                ConfirmActionProp.Set(null);
            else
                ConfirmActionProp.Set(Submit);
        }

        private async void Submit()
        {
            try
            {
                m_CancellationTokenSource = new CancellationTokenSource();
                IsLoadingProp.Set(true);
                await Task.Delay(3000, m_CancellationTokenSource.Token);
                Submitted?.Invoke(this);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                IsLoadingProp.Set(false);
            }
        }
    }
}