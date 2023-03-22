using System;
using System.Collections.Generic;
using YADBF.Unity;

namespace YADBF
{
    public sealed class ObservableProperty<T> : IReadOnlyObservableProperty<T>
    {
        public delegate void ValueChangedCallback(ObservableProperty<T> property, T prevValue, T currValue);

        public event ValueChangedCallback ValueChanged; 

        private T m_Value;
        public T Value
        {
            get => m_Value;
            set => Set(value);
        }

        private IBinding m_Binding;
        
        public ObservableProperty(T defaultValue = default)
        {
            m_Value = defaultValue;
        }
        
        public void Set(T value)
        {
            if (EqualityComparer<T>.Default.Equals(m_Value, value))
                return;
            
            var prevValue = m_Value;
            m_Value = value;
            ValueChanged?.Invoke(this, prevValue, value);
        }

        public void Bind(ObservableProperty<T> prop)
        {
            if (m_Binding != null)
                m_Binding.Dispose();
            m_Binding = new ObservablePropertyBinding<T>(prop, Set);
            Set(prop.Value);
        }

        public void Bind<TOther>(ObservableProperty<TOther> prop, Func<TOther, T> converter)
        {
            if (m_Binding != null)
                m_Binding.Dispose();
            m_Binding = new ObservablePropertyBinding<TOther>(prop, 
                other => Set(converter.Invoke(other)));
            Set(converter.Invoke(prop.Value));
        }

        public void Unbind()
        {
            m_Binding.Dispose();
            m_Binding = null;
        }

        public override string ToString()
        {
            return Value?.ToString();
        }
    }
}