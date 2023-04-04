using System;
using YADBF;

namespace Login
{
    public interface ISignUpConfirmationForm
    {
        event Action<ISignUpConfirmationForm> FormSubmitted;
        
        ObservableProperty<bool> IsLoadingProp { get; }
        ObservableProperty<Action> ConfirmActionProp { get; }
        ObservableProperty<string> ConfirmationCodeTextProp { get; }
        
        void Cancel();
    }
}