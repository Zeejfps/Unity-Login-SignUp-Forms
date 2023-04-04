using System;
using YADBF;

public interface IToggleWidget : IWidget
{
    event Action<IToggleWidget> Clicked;
    
    ObservableProperty<bool> IsOnProp { get; }
    ObservableProperty<bool> IsInteractable { get; }
    
    void HandleClick();
}