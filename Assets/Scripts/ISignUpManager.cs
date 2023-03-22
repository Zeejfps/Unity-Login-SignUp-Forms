using System;
using System.Threading.Tasks;
using YADBF;

namespace Login
{
    public interface ISignUpManager
    {
        ObservableProperty<bool> IsLoadingProp { get; }
        ObservableProperty<string> EmailProp { get; }
        ObservableProperty<string> PasswordProp { get; }
        ObservableProperty<string> ConfirmPasswordProp { get; }
        ObservableProperty<Action> SignUpActionProp { get; }
    }
}