using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using YADBF;

namespace Login
{
    public sealed class TestSignUpConfirmationFlow : ISignUpConfirmationFlow
    {
        public event Action<ISignUpConfirmationFlow> Completed;
        public event Action<ISignUpConfirmationFlow> Canceled;
        
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<Action> ConfirmActionProp { get; } = new();
        public ObservableProperty<string> ConfirmationCodeTextProp { get; } = new();

        private CancellationTokenSource m_CancellationTokenSource;
        
        public void Cancel()
        {
            m_CancellationTokenSource?.Cancel();
        }

        public TestSignUpConfirmationFlow()
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
                ConfirmActionProp.Set(ConfirmSignUp);
        }

        private async void ConfirmSignUp()
        {
            try
            {
                m_CancellationTokenSource = new CancellationTokenSource();
                IsLoadingProp.Set(true);
                await Task.Delay(3000, m_CancellationTokenSource.Token);
                Debug.Log("?");
                Completed?.Invoke(this);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                Canceled?.Invoke(this);
            }
            finally
            {
                IsLoadingProp.Set(false);
            }
        }
    }
}