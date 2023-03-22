using System;
using YADBF;

public interface ILoginFormWidget : IWidget
{
    ITextInputWidget EmailInputWidget { get; }
    IPasswordFieldWidget PasswordInputWidget { get; }
    ObservableProperty<Action> LoginActionProp { get; }
}