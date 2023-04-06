using System;
using System.Collections;
using System.Collections.Generic;
using YADBF;

public sealed class ListWidget<T> : IListWidget<T> where T : IWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    
    private readonly List<T> m_Items = new();

    private readonly List<Action<object>> m_Listeners = new();
    private readonly List<Action<T>> m_GenericListeners = new();

    event Action<object> IListWidget.ItemAdded
    {
        add => m_Listeners.Add(value);
        remove => m_Listeners.Remove(value);
    }

    event Action<T> IListWidget<T>.ItemAdded
    {
        add => m_GenericListeners.Add(value);
        remove => m_GenericListeners.Remove(value);
    }

    IList IListWidget.Items => m_Items;
    IReadOnlyList<T> IListWidget<T>.Items => m_Items;

    public void Add(T itemWidget)
    {
        m_Items.Add(itemWidget);
        foreach (var listener in m_Listeners)
            listener.Invoke(itemWidget);
        foreach (var listener in m_GenericListeners)
            listener.Invoke(itemWidget);
    }
    
}

internal sealed class ListWidget : IListWidget
{
    public event Action<object> ItemAdded;

    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);

    public IList Items => m_Items;

    private readonly List<object> m_Items = new();

    public void Add(object itemWidget)
    {
        m_Items.Add(itemWidget);
        ItemAdded?.Invoke(itemWidget);
    }
}