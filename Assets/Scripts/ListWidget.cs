using System;
using System.Collections.Generic;
using YADBF;

internal sealed class ListWidget : IListWidget
{
    public event Action<object> ItemAdded;

    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);

    public IEnumerable<object> Items => m_Items;

    private readonly IList<object> m_Items = new List<object>();

    public void Add(object itemWidget)
    {
        m_Items.Add(itemWidget);
        ItemAdded?.Invoke(itemWidget);
    }
}