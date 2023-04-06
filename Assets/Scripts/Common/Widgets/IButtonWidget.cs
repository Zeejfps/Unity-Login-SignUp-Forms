using System;
using YADBF;

namespace Common.Widgets
{
    public interface IButtonWidget : IWidget, IInteractableWidget
    {
        ObservableProperty<Action> ActionProp { get; }
        ObservableProperty<bool> IsLoadingProp { get; }
    }
}