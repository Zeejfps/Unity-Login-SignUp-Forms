using System;
using YADBF;

public interface IButtonWidget : IWidget, IFocusable
{
    ObservableProperty<bool> IsInteractableProp { get; }
    ObservableProperty<Action> ActionProp { get; }
    ObservableProperty<bool> IsLoadingProp { get; }
}