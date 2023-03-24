using System;
using YADBF;

public interface IButtonWidget : IWidget
{
    ObservableProperty<bool> IsInteractableProp { get; }
    ObservableProperty<Action> ActionProp { get; }
    ObservableProperty<bool> IsLoadingProp { get; }
}