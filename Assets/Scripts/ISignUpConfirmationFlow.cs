using System;
using YADBF;

namespace Login
{
    public interface ISignUpConfirmationFlow
    {
        event Action<ISignUpConfirmationFlow> Completed;
        event Action<ISignUpConfirmationFlow> Canceled;
        
        ObservableProperty<bool> IsLoadingProp { get; }
        ObservableProperty<Action> ConfirmActionProp { get; }
        ObservableProperty<string> ConfirmationCodeTextProp { get; }
        
        void Cancel();
    }
}