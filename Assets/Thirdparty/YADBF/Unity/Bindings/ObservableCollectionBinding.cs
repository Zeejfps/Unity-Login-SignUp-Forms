using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

namespace YADBF.Unity
{
    public class ObservableCollectionBinding<TModel> : 
        PropertyBinding<ObservableCollection<TModel>> 
        where TModel : class
    {
        [SerializeField] private Transform m_ItemContainer;
        [SerializeField] private View<TModel> m_ItemViewPrefab;

        private readonly Dictionary<TModel, View<TModel>> m_ViewModelToViewMap = new();

        protected override void OnPropertyValueChanged(
            ObservableCollection<TModel> prevValue, ObservableCollection<TModel> currValue)
        {
            if (prevValue != null)
            {
                prevValue.CollectionChanged -= OnCollectionChanged;
                DestroyViews();
            }

            if (currValue != null)
            {
                currValue.CollectionChanged += OnCollectionChanged;
                CreateViews(currValue);
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Collection_OnItemAdded((TModel)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Collection_OnItemRemoved((TModel)e.OldItems[0]);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Collection_OnReset();
                    break;
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Replace:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Collection_OnItemAdded(TModel model)
        {
            CreateView(model);
        }

        private void Collection_OnItemRemoved(TModel viewModel)
        {
            var itemView = m_ViewModelToViewMap[viewModel];
            DestroyView(itemView);
            m_ViewModelToViewMap.Remove(viewModel);
        }

        private void Collection_OnReset()
        {
            DestroyViews();
        }

        private void CreateViews(ObservableCollection<TModel> collection)
        {
            foreach (var model in collection)
                CreateView(model);
        }

        private void DestroyViews()
        {
            foreach (var itemView in m_ViewModelToViewMap.Values)
                DestroyView(itemView);
            m_ViewModelToViewMap.Clear();
        }

        private void CreateView(TModel model)
        {
            var itemView = Instantiate(m_ItemViewPrefab, m_ItemContainer);
            itemView.Model = model;
            m_ViewModelToViewMap[model] = itemView;
        }

        private void DestroyView(View<TModel> itemView)
        {
            var go = itemView.gameObject;
            go.SetActive(false);
            Destroy(go);
        }
    }
}
