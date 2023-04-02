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
        
        private RectTransform RectTransform { get; set; }

        protected override void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            base.Awake();
        }

        
        protected override void OnBindToModel(IPasswordFieldWidget model)
        {
            base.OnBindToModel(model);
            m_TextInputWidgetView.Model = model.TextInputWidget;
            m_ShowPasswordToggle.Model = model.ShowPasswordToggleWidget;
            Bind(model.ErrorTextProperty, value =>
            {
                var showHighlight = !string.IsNullOrWhiteSpace(value);
                m_ErrorText.SetText(value);
                m_ErrorHighlight.SetActive(showHighlight);
                RebuildLayout();
            });
        }

        private void RebuildLayout()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(m_ErrorText.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(RectTransform);

            var parent = transform.parent;
            if (parent == null)
                return;
        
            var parentLayoutGroup = parent.GetComponentInParent<LayoutGroup>();
            if (parentLayoutGroup == null)
                return;
        
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentLayoutGroup.transform);
        }
    }
}