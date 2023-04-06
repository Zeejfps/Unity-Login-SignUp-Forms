using System;
using System.Collections.Generic;
using YADBF;

internal sealed class ListWidget : IListWidget
{
    public event Action<object> ItemAdded;

    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);

    public IReadOnlyList<object> Items => m_Items;

    private readonly List<object> m_Items = new();

    public void Add(object itemWidget)
    {
        m_Items.Add(itemWidget);
        ItemAdded?.Invoke(itemWidget);
    }
}