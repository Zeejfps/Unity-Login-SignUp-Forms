using System.Collections.Generic;
using YADBF;

public sealed class TabGroup : ITabGroup
{
    private readonly List<ITabWidget> m_Tabs = new();

    public void AddTab(ITabWidget tabWidget)
    {
        tabWidget.IsSelectedProp.ValueChanged += TabWidget_IsSelected_OnValueChanged;
        m_Tabs.Add(tabWidget);
    }

    private void TabWidget_IsSelected_OnValueChanged(ObservableProperty<bool> property, bool wasSelected, bool isSelected)
    {
        if (!isSelected)
            return;
        
        foreach (var tab in m_Tabs)
        {
            if (tab.IsSelectedProp == property)
                continue;
            tab.IsSelectedProp.Set(false);
        }
    }
}