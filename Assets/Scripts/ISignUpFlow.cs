using System;
using System.Threading.Tasks;
using YADBF;

namespace Login
{
    public interface ISignUpFlow
    {
        event Action Completed;
        
        ObservableProperty<bool> IsLoadingProp { get; }
        ObservableProperty<string> EmailProp { get; }
        ObservableProperty<string> PasswordProp { get; }
        ObservableProperty<string> ConfirmPasswordProp { get; }
        ObservableProperty<Action> SignUpActionProp { get; }
    }
}