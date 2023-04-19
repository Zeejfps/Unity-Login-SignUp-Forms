using UnityEngine;
using YADBF.Unity;

namespace Bindings
{
    public abstract class WidgetViewBinding<TWidgetView, TWidget> : MonoBehaviour 
        where TWidgetView : View<TWidget> 
        where TWidget : class, IWidget
    {
        [SerializeField] private string m_WidgetId;
        [SerializeField] private TWidgetView m_WidgetView;
        
        private void Reset()
        {
            m_WidgetView = GetComponent<TWidgetView>();
        }
        
        private void Start()
        {
            var provider = GetComponentInParent<IWidgetProvider>();
            var widget = provider.Get<TWidget>(m_WidgetId);
            m_WidgetView.Model = widget;
        }
    }
}