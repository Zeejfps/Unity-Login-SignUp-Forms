using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.Widgets
{
    public interface IListWidget<TItemWidget> : IListWidget where TItemWidget : IWidget
    {
        new event Action<TItemWidget> ItemAdded;
        new IReadOnlyList<TItemWidget> Items { get; }

        void Add(TItemWidget itemWidget);
    }

    public interface IListWidget : IWidget
    {
        event Action<object> ItemAdded;
        IList Items { get; }
    }
}