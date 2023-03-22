using UnityEngine;
using YADBF.Unity;

namespace Login
{
    public abstract class UGUIWidgetView<TWidget> : View<TWidget> where TWidget : class, IWidget
    {
        [SerializeField] private UGUIThemeProvider m_ThemeProvider;

        protected UGUITheme Theme => m_ThemeProvider.Theme;
        
        protected override void OnBindToModel(TWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.IsVisibleProp, gameObject.SetActive);
        }

        protected override void Reset()
        {
            base.Reset();
            m_ThemeProvider = FindObjectOfType<UGUIThemeProvider>();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (m_ThemeProvider != null)
                ApplyTheme(m_ThemeProvider.Theme);
        }
        
        protected virtual void ApplyTheme(UGUITheme theme) {}
    }
}