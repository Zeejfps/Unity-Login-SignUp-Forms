using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YADBF.Unity;

public sealed class TabWidgetView : View<ITabWidget>, IPointerClickHandler
{
    [SerializeField] private Image m_BackgroundImage;
    [SerializeField] private Color m_DefaultColor = Color.white;
    [SerializeField] private Color m_SelectedColor = Color.grey;

    protected override void OnBindToModel(ITabWidget model)
    {
        base.OnBindToModel(model);
        Bind(model.IsSelectedProp, isTrue =>
        {
            if (isTrue)
                m_BackgroundImage.color = m_SelectedColor;
            else
                m_BackgroundImage.color = m_DefaultColor;
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Model?.HandleClick();
    }
}