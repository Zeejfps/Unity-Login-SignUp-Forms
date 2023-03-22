using System;
using UnityEngine;

namespace YADBF.Unity
{
    [Serializable]
    public sealed class PropertyBindingSettings<TValue> : PropertyBindingSettings
    {
        private Type m_PropertyType = typeof(ObservableProperty<TValue>);
        
        protected override Type GetPropertyType()
        {
            return m_PropertyType;
        }
    }
    
    [Serializable]
    public abstract class PropertyBindingSettings : ISerializationCallbackReceiver
    {
        public View View
        {
            get => m_View;
            set => m_View = value;
        }

        public string PropertyName
        {
            get => m_PropertyName;
            set => m_PropertyName = value;
        }
        
        [SerializeField] private View m_View;
        [SerializeField] private string m_PropertyName;
        
        // Needed for Inspector
        [HideInInspector] 
        [SerializeField] private string m_PropertyTypeName;
        
        public void OnBeforeSerialize()
        {
            m_PropertyTypeName = GetPropertyType().AssemblyQualifiedName;
        }

        public void OnAfterDeserialize()
        {
            m_PropertyTypeName = GetPropertyType().AssemblyQualifiedName;
        }

        // Needed for Inspector
        protected abstract Type GetPropertyType();
    }
}