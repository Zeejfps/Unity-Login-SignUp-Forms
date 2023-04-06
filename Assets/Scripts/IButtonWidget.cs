using System;
using YADBF;

public interface IButtonWidget : IWidget, IInteractable
{
    ObservableProperty<Action> ActionProp { get; }
    ObservableProperty<bool> IsLoadingProp { get; }
}