using Login;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        Model?.HandleClick();
    }
}