using System.Collections.Generic;

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

        public override string ToString()
        {
            return Value?.ToString();
        }
    }
}