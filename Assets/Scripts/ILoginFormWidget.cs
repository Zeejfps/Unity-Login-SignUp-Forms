using System;
using YADBF;

public interface ILoginFormWidget : IWidget
{
    ObservableProperty<ITextInputWidget> EmailInputWidgetProp { get; }
    ObservableProperty<IPasswordInputWidget> PasswordInputWidgetProp { get; }
    ObservableProperty<Action> LoginActionProp { get; }
}