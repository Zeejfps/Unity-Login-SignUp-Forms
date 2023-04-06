using Common.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UGUI
{
    public sealed class UGUIPasswordRequirementWidgetView : UGUIWidgetView<IPasswordRequirementWidget>
    {
        [SerializeField] private TMP_Text m_Text;
        [SerializeField] private Image m_Icon;
        [SerializeField] private Sprite m_IsMetSprite;
        [SerializeField] private Sprite m_IsNotMetSprite;

        protected override void OnBindToModel(IPasswordRequirementWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.Description, value =>
            {
                m_Text.text = value;
            });
            Bind(model.IsMet, isMet =>
            {
                var color = isMet ? Theme.HighlightColor : Theme.ErrorColor;
                var sprite = isMet ? m_IsMetSprite : m_IsNotMetSprite;
                m_Text.color = color;
                m_Icon.color = color;
                m_Icon.sprite = sprite;
            });
        }
    }
}