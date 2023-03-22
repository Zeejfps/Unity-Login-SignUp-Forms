using System;
using YADBF;

public interface ILoginFormWidget : IWidget
{
    ITextInputWidget EmailInputWidget { get; }
    IPasswordInputWidget PasswordInputWidget { get; }
    ObservableProperty<Action> LoginActionProp { get; }
}