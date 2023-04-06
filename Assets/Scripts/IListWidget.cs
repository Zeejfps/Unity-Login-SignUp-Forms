using System;
using System.Collections.Generic;

public interface IListWidget<TItemWidget> : IListWidget where TItemWidget : IWidget
{
    
}

public interface IListWidget : IWidget
{
    event Action<object> ItemAdded;
    IReadOnlyList<object> Items { get; }

    void Add(object itemWidget);
}