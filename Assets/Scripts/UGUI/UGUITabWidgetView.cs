using Common.Widgets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UGUI
{
    public sealed class UGUITabWidgetView : UGUIWidgetView<ITabWidget>, IPointerClickHandler
    {
        [SerializeField] private Image m_BackgroundImage;
        protected override void OnBindToModel(ITabWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.IsSelectedProp, isTrue =>
            {
                if (isTrue)
                    m_BackgroundImage.color = Theme.TabStyle.DefaultColor;
                else
                    m_BackgroundImage.color = Theme.TabStyle.SelectedColor;
            });
        }

        protected override void ApplyTheme(UGUITheme theme)
        {
            base.ApplyTheme(theme);
            m_BackgroundImage.color = Theme.TabStyle.DefaultColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Model?.HandleClick();
        }
    }
}