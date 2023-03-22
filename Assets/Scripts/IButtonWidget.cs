using System;
using YADBF;

public interface IButtonWidget : IWidget
{
    ObservableProperty<bool> IsInteractable { get; }
    ObservableProperty<Action> ActionProp { get; }
}