using System;
using System.Collections.Generic;

public interface IListWidget<TItemWidget> : IListWidget where TItemWidget : IWidget
{
    
}

public interface IListWidget : IWidget
{
    event Action<object> ItemAdded;
    IEnumerable<object> Items { get; }

    void Add(object itemWidget);
}