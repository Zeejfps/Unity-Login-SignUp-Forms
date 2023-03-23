using System;
using YADBF;

public interface ILoginFlow
{
    ObservableProperty<string> EmailProp { get; }
    ObservableProperty<string> PasswordProp { get; }
    ObservableProperty<bool> IsLoadingProp { get; }
    ObservableProperty<Action> LoginActionProp { get; }
}