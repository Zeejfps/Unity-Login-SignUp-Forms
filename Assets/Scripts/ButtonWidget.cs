using System;
using YADBF;

public sealed class ButtonWidget : IButtonWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<bool> IsInteractableProp { get; } = new();
    public ObservableProperty<Action> ActionProp { get; } = new();
    public ObservableProperty<bool> IsLoadingProp { get; } = new();
    public ObservableProperty<bool> IsFocusedProperty { get; } = new();
}