using System;
using YADBF;

namespace Login
{
    public interface ISignUpConfirmation
    {
        event Action Confirmed;
        
        ObservableProperty<bool> IsLoadingProp { get; }
        ObservableProperty<Action> ConfirmActionProp { get; }
        ObservableProperty<string> ConfirmationCodeTextProp { get; }
        
        void Cancel();
    }
}