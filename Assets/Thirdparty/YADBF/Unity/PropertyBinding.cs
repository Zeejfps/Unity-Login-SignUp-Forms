using UnityEngine;

namespace YADBF.Unity
{
    public abstract class PropertyBinding<T> : MonoBehaviour
    {
        [SerializeField] private PropertyBindingSettings<T> m_PropertyBindingSettings = new();

        private readonly YADBF.PropertyBinding<T> m_PropertyBinding = new();

        protected T PropertyValue
        {
            get => m_PropertyBinding.Property.Value;
            set => m_PropertyBinding.Property.Value = value;
        }
        
        protected virtual void Awake() {}
        
        protected virtual void OnEnable()
        {
            if (!Application.isPlaying)
                return;
            BindToView();
        }

        protected virtual void Start()
        {
        }

        protected virtual void OnDisable()
        {
            if (!Application.isPlaying)
                return;
            
            UnbindFromView();
        }

        protected virtual void OnDestroy()
        {
        }

        protected virtual void Reset()
        {
            m_PropertyBindingSettings.View = GetComponentInParent<View>();
        }

        protected void BindToView()
        {
            var view = m_PropertyBindingSettings.View;
            var viewModel = view.GetViewModel();
            view.ModelChanged += View_OnViewModelChanged;
            if (viewModel != null) Bind(viewModel);
        }

        protected void UnbindFromView()
        {
            var view = m_PropertyBindingSettings.View;
            view.ModelChanged -= View_OnViewModelChanged;
            Unbind();
        }
        
        private void View_OnViewModelChanged()
        {
            Unbind();
            var model = m_PropertyBindingSettings.View.GetViewModel();
            if (model != null) Bind(model);
        }

        private void Bind(object viewModel)
        {
            var propertyBinding = m_PropertyBinding;
            var propertyName = m_PropertyBindingSettings.PropertyName;
            propertyBinding.Bind(viewModel, propertyName);
            propertyBinding.Property.ValueChanged += PropertyBinding_OnPropertyValueChanged;
            OnBound(propertyBinding.Property.Value);
        }

        private void Unbind()
        {
            var propertyBinding = m_PropertyBinding;
            if (propertyBinding != null && propertyBinding.IsBound)
            {
                propertyBinding.Property.ValueChanged -= PropertyBinding_OnPropertyValueChanged;
                propertyBinding.Unbind();
                OnUnbound();
            }
        }

        protected virtual void OnBound(T currValue)
        {
            OnPropertyValueChanged(default, currValue);
        }
        
        protected virtual void OnUnbound() {}

        private void PropertyBinding_OnPropertyValueChanged(ObservableProperty<T> property, T prevValue, T currValue)
        {
            OnPropertyValueChanged(prevValue, currValue);
        }

        protected abstract void OnPropertyValueChanged(T prevValue, T currValue);
    }
}