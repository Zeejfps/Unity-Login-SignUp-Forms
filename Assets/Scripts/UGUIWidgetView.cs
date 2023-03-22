using UnityEngine;
using YADBF.Unity;

namespace Login
{
    public abstract class UGUIWidgetView<TWidget> : View<TWidget> where TWidget : class, IWidget
    {
        [SerializeField] private UGUITheme m_Theme;

        protected UGUITheme Theme => m_Theme;
        
        protected override void OnBindToModel(TWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.IsVisibleProp, gameObject.SetActive);
        }
    }
}