using System;
using YADBF;

namespace Login
{
    public interface ISignUpForm
    {
        event Action Completed;
        
        ObservableProperty<bool> IsLoadingProp { get; }
        ObservableProperty<string> EmailProp { get; }
        ObservableProperty<string> PasswordProp { get; }
        ObservableProperty<string> ConfirmPasswordProp { get; }
        ObservableProperty<Action> SubmitActionProp { get; }
    }
}