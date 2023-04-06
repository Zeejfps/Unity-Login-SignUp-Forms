using System;
using YADBF;

public sealed class ActionPropertyButtonWidget : IButtonWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsInteractableProp { get; } = new();
    public ObservableProperty<Action> ActionProp { get; }
    public ObservableProperty<bool> IsLoadingProp { get; } = new();
    public ObservableProperty<bool> IsFocusedProperty { get; } = new();

    public ActionPropertyButtonWidget(ObservableProperty<Action> actionProp)
    {
        ActionProp = actionProp;
        IsInteractableProp.Bind(ActionProp, value => value != null);
    }

}