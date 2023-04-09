using Common.Widgets;
using InfoPopup;
using Login;
using TMPro;
using UnityEngine;

namespace UGUI
{
    public sealed class UGUIInfoPopupWidgetView : UGUIWidgetView<IInfoPopupWidget>
    {
        [SerializeField] private TMP_Text m_TitleText;
        [SerializeField] private TMP_Text m_ContentText;
        [SerializeField] private UGUIButtonWidgetView m_ButtonWidgetView;
    
        protected override void OnBindToModel(IInfoPopupWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.TitleTextProp, value =>
            {
                m_TitleText.SetText(value);
            });

            Bind(model.InfoTextProp, value =>
            {
                m_ContentText.text = value;
                
            });
            m_ButtonWidgetView.Model = model.OkButtonWidget;
        }
    }
}