using System;
using YADBF;

namespace Login
{
    public interface ISignUpConfirmationManager 
    {
        ObservableProperty<bool> IsLoadingProp { get; }
        ObservableProperty<Action> ConfirmActionProp { get; }
        ObservableProperty<string> ConfirmationCodeTextProp { get; }
    }
}