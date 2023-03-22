using System;
using System.Threading.Tasks;
using YADBF;

namespace Login
{
    public interface ILoginManager
    {
        ObservableProperty<string> EmailProp { get; }
        ObservableProperty<string> PasswordProp { get; }
        ObservableProperty<bool> IsLoadingProp { get; }
        ObservableProperty<Action> LoginActionProp { get; }

        Task LoginAsync(string email, string password);
    }
}