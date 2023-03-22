using System;
using YADBF;

public interface IWidget
{
    ObservableProperty<bool> IsVisibleProp { get; }
}