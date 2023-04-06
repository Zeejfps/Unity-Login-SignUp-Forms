using System;
using System.Collections.Generic;
using UnityEngine;
using YADBF.Unity;

namespace UGUI
{
    public interface ITabGroupWidget : IWidget
    {
        ITabWidget[] TabWidgets { get; }
    }

    [Serializable]
    public sealed class TabToContentMap
    {
        public View<ITabWidget> m_TabView;
        public View m_ContentView;
    }

    public sealed class UGUITabGroupWidgetView : UGUIWidgetView<ITabGroupWidget>
    {
        [SerializeField] private List<TabToContentMap> m_TabWidgetViews;

        protected override void OnBindToModel(ITabGroupWidget model)
        {
            base.OnBindToModel(model);
        }
    }
}