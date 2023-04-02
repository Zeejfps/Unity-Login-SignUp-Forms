using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Login
{
    public sealed class UGUIPasswordFieldWidgetView : UGUIWidgetView<IPasswordFieldWidget>
    {
        [SerializeField] private UGUITextInputWidgetView m_TextInputWidgetView;
        [SerializeField] private UGUIToggleWidgetView m_ShowPasswordToggle;
        [SerializeField] private TMP_Text m_ErrorText;
        [SerializeField] private GameObject m_ErrorHighlight;

        private bool m_IsShowingPassword;
        
        protected override void OnBindToModel(IPasswordFieldWidget model)
        {
            base.OnBindToModel(model);
            m_TextInputWidgetView.Model = model.TextInputWidget;
            m_ShowPasswordToggle.Model = model.ShowPasswordToggleWidget;
            Bind(model.ErrorTextProperty, value =>
            {
                var showHighlight = !string.IsNullOrWhiteSpace(value);
                m_ErrorText.SetText(value);
                LayoutRebuilder.ForceRebuildLayoutImmediate(m_ErrorText.rectTransform);
                m_ErrorHighlight.SetActive(showHighlight);
            });
        }
    }
}