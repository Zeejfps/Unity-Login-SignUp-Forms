using System.Collections.Generic;
using Common.Widgets;
using YADBF;

namespace Common.Controllers
{
    public sealed class TabGroupController : ITabGroupController
    {
        private readonly List<TabToContentLink> m_Links = new();

        public void LinkTabToContent(ITabWidget tabWidget, IWidget contentWidget)
        {
            var tabToContentLink = new TabToContentLink(tabWidget, contentWidget);
            tabWidget.IsSelectedProp.ValueChanged += TabWidget_IsSelected_OnValueChanged;
            m_Links.Add(tabToContentLink);
        }

        public void Dispose()
        {
            foreach (var link in m_Links)
                link.Dispose();
            m_Links.Clear();
        }

        private void TabWidget_IsSelected_OnValueChanged(ObservableProperty<bool> property, bool wasSelected, bool isSelected)
        {
            if (!isSelected)
                return;
        
            foreach (var link in m_Links)
            {
                if (link.TabWidget.IsSelectedProp == property)
                    continue;
                link.TabWidget.IsSelectedProp.Set(false);
            }
        }
    }

    sealed class TabToContentLink
    {
        public ITabWidget TabWidget { get; }
        public IWidget ContentWidget { get; }
    
        public TabToContentLink(ITabWidget tabWidget, IWidget contentWidget)
        {
            TabWidget = tabWidget;
            ContentWidget = contentWidget;
            ContentWidget.IsVisibleProp.Set(TabWidget.IsSelectedProp.Value);

            TabWidget.IsSelectedProp.ValueChanged += IsSelectedProp_OnValueChanged;
        }

        public void Dispose()
        {
            TabWidget.IsSelectedProp.ValueChanged -= IsSelectedProp_OnValueChanged;
        }

        private void IsSelectedProp_OnValueChanged(ObservableProperty<bool> property, bool wasSelected, bool isSelected)
        {
            if (isSelected)
                ContentWidget.IsVisibleProp.Set(true);
            else
                ContentWidget.IsVisibleProp.Set(false);
        }
    }
}