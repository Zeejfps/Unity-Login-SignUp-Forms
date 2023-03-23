using System;
using System.Threading.Tasks;
using YADBF;

namespace Login
{
    public sealed class TestSignUpConfirmationManager : ISignUpConfirmationManager
    {
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<Action> ConfirmActionProp { get; } = new();
        public ObservableProperty<string> ConfirmationCodeTextProp { get; } = new();

        public TestSignUpConfirmationManager()
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
            IsLoadingProp.Set(true);
            await Task.Delay(2000);
            IsLoadingProp.Set(false);
        }
    }
}