using System;
using YADBF;

public interface ILoginForm
{
    ObservableProperty<string> EmailProp { get; }
    ObservableProperty<string> PasswordProp { get; }
    ObservableProperty<bool> IsLoadingProp { get; }
    ObservableProperty<Action> LoginActionProp { get; }
}