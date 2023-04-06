using System.Collections.Generic;
using Common.Widgets;
using UnityEngine;
using UnityEngine.UI;
using YADBF.Unity;

namespace UGUI
{
    public sealed class UGUIListWidgetView : UGUIWidgetView<IListWidget>
    {
        [SerializeField] private RectTransform m_Container;
        [SerializeField] private View m_WidgetViewPrefab;

        private readonly Dictionary<object, View> m_ItemToViewMap = new();

        protected override void OnBindToModel(IListWidget model)
        {
            base.OnBindToModel(model);
            model.ItemAdded += Model_OnItemAdded;
            foreach (var item in model.Items)
                Model_OnItemAdded(item);
        }

        protected override void OnUnbindFromModel(IListWidget model)
        {
            model.ItemAdded -= Model_OnItemAdded;
            base.OnUnbindFromModel(model);
        }

        private void Model_OnItemAdded(object item)
        {
            var view = Instantiate(m_WidgetViewPrefab, m_Container);
            view.TrySetViewModel(item);
            m_ItemToViewMap.Add(item, view);
            LayoutRebuilder.ForceRebuildLayoutImmediate(m_Container);
        }
    }
}