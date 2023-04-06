using System;
using YADBF;

namespace Common.Widgets
{
    public sealed class ActionPropertyButtonWidget : IButtonWidget
    {
        public ObservableProperty<bool> IsVisibleProperty { get; } = new(true);
        public ObservableProperty<Action> ActionProp { get; }
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<bool> IsFocusedProperty { get; } = new();
        public ObservableProperty<bool> IsInteractableProperty { get; } = new(true);

        public ActionPropertyButtonWidget(ObservableProperty<Action> actionProp)
        {
            ActionProp = actionProp;
        }

    }
}