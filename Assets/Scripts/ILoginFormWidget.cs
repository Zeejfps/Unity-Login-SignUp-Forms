using System;
using YADBF;

public interface ILoginFormWidget : IWidget
{
    ObservableProperty<ITextInputWidget> EmailInputWidgetProp { get; }
    ObservableProperty<ITextInputWidget> PasswordInputWidgetProp { get; }
    ObservableProperty<Action> LoginActionProp { get; }
}