using System;
using System.Collections.Generic;
using UnityEngine;

namespace YADBF.Unity
{
    public abstract class View<TModel> : View
        where TModel : class
    {
        private TModel m_Model;
        public TModel Model
        {
            get => m_Model;
            set
            {
                if (m_Model == value)
                    return;

                var prevViewModel = m_Model;
                m_Model = value;

                OnModelChanged(prevViewModel, m_Model);
            }
        }

        private readonly List<IBinding> m_Bindings = new();
        private readonly Type m_ModelType = typeof(TModel);

        protected virtual void Awake() {}
        protected virtual void OnEnable() {}
        protected virtual void Start() {}
        protected virtual void OnDisable() {}
        protected virtual void OnDestroy()
        {
            ClearBindings();
        }
        
        protected virtual void Reset() {}

        protected virtual void OnModelChanged(TModel prevModel, TModel currModel)
        {
            if (prevModel != null)
                UnbindFromModel(prevModel);
            
            if (currModel != null)
                BindToModel(currModel);
            
            RaiseModelChangedEvent();
        }
        
        public sealed override object GetViewModel()
        {
            return Model;
        }

        public sealed override bool TrySetViewModel(object viewModel)
        {
            if (viewModel is TModel myViewModel)
            {
                Model = myViewModel;
                return true;
            }
            return false;
        }

        private void BindToModel(TModel model)
        {
            OnBindToModel(model);
        }
        
        private void UnbindFromModel(TModel model)
        {
            OnUnbindFromModel(model);
            ClearBindings();
        }

        private void ClearBindings()
        {
            foreach (var binding in m_Bindings)
                binding.Dispose();
            m_Bindings.Clear();
        }

        public sealed override Type GetViewModelType()
        {
            return m_ModelType;
        }

        protected virtual void OnBindToModel(TModel model)
        {
            
        }

        protected virtual void OnUnbindFromModel(TModel model)
        {
            
        }

        protected IBinding Bind<T>(ObservableProperty<T> prop, Action<T> listener)
        {
            var binding = new ObservablePropertyBinding<T>(prop, listener);
            m_Bindings.Add(binding);
            listener.Invoke(prop.Value);
            return binding;
        }

        protected IBinding Bind<T>(ObservableProperty<T> prop, View<T> view) where T : class
        {
            var binding = new ObservablePropertyBinding<T>(prop, model => view.Model = model);
            m_Bindings.Add(binding);
            view.Model = prop.Value;
            return binding;
        }
    }

    public abstract class View : MonoBehaviour
    {
        public event Action ModelChanged;
        public abstract bool TrySetViewModel(object viewModel);
        public abstract object GetViewModel();
        public abstract Type GetViewModelType();

        protected void RaiseModelChangedEvent()
        {
            ModelChanged?.Invoke();
        }
    }
}