using System;
using System.Threading.Tasks;
using YADBF;

namespace Login
{
    public sealed class BasicSignUpManager : ISignUpManager
    {
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<string> EmailProp { get; } = new();
        public ObservableProperty<string> PasswordProp { get; } = new();
        public ObservableProperty<string> ConfirmPasswordProp { get; } = new();
        public ObservableProperty<Action> SignUpActionProp { get; } = new();
    }
}