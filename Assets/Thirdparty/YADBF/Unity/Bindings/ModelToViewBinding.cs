using UnityEngine;

namespace YADBF.Unity.Bindings
{
    public abstract class ModelToViewBinding<TModel> : PropertyBinding<TModel> 
        where TModel : class
    {
        [SerializeField] private View<TModel> m_View;
        
        protected override void OnPropertyValueChanged(TModel prevValue, TModel currValue)
        {
            m_View.Model = currValue;
        }
    }
}
