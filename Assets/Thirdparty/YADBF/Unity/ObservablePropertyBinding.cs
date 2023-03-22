using System;

namespace YADBF.Unity
{
    internal sealed class ObservablePropertyBinding<T> : IBinding
    {
        private readonly Action<T> m_Listener;
        private readonly ObservableProperty<T> m_ObservableProperty;

        private bool m_IsDisposed;
        
        public ObservablePropertyBinding(ObservableProperty<T> observableProperty, Action<T> listener)
        {
            m_Listener = listener;
            m_ObservableProperty = observableProperty;
            m_ObservableProperty.ValueChanged += ObservableProperty_OnValueChanged;
        }

        ~ObservablePropertyBinding() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        private void Dispose(bool disposing)
        {
            if (!m_IsDisposed)
            {
                if (disposing)
                {
                    m_ObservableProperty.ValueChanged -= ObservableProperty_OnValueChanged;
                }

                m_IsDisposed = true;
            }
        }

        private void ObservableProperty_OnValueChanged(ObservableProperty<T> property, T prevvalue, T currvalue)
        {
            m_Listener.Invoke(currvalue);
        }
    }
}