using System;
using YADBF;

public sealed class ButtonWidget : IButtonWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<Action> ActionProp { get; } = new();
    public ObservableProperty<bool> IsLoadingProp { get; } = new();
    public ObservableProperty<bool> IsFocusedProperty { get; } = new();
    public ObservableProperty<bool> IsInteractableProperty { get; } = new();
}