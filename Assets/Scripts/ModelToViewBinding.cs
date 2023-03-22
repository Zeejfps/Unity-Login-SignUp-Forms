using UnityEngine;
using YADBF.Unity;

public abstract class ModelToViewBinding<TModel> : PropertyBinding<TModel> where TModel : class
{
    [SerializeField] private View<TModel> m_ViewPrefab;

    private View<TModel> m_View;

    protected override void OnPropertyValueChanged(TModel prevValue, TModel currValue)
    {
        if (currValue == null && m_View != null)
        {
            DestroyView();
            return;
        }
        
        if (currValue != null && m_View != null)
        {
            m_View.Model = currValue;
            return;
        }

        if (currValue != null && m_View == null)
        {
            InstantiateView(currValue);
            return;
        }
    }

    private void InstantiateView(TModel model)
    {
        m_View = Instantiate(m_ViewPrefab, transform);
        m_View.Model = model;
    }

    private void DestroyView()
    {
        var go = m_View.gameObject;
        go.SetActive(false);
        Destroy(go);
        m_View = null;
    }
}