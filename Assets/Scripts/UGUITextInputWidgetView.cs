using TMPro;
using UnityEngine;

namespace Login
{
    public sealed class UGUITextInputWidgetView : UGUIWidgetView<ITextInputWidget>
    {
        [SerializeField] private TMP_InputField m_InputField;
        [SerializeField] private TMP_Text m_ErrorText;
        
        private TMP_InputField.ContentType m_CachedContentType;

        protected override void Awake()
        {
            base.Awake();
            m_CachedContentType = m_InputField.contentType;
        }

        protected override void OnBindToModel(ITextInputWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.TextProp, m_InputField.SetTextWithoutNotify);
            Bind(model.IsInteractableProperty, UpdateInteractableState);
            Bind(model.IsMaskingCharactersProperty, UpdateCharacterMaskingState);
            Bind(model.ErrorTextProperty, UpdateErrorText);
            m_InputField.onValueChanged.AddListener(InputField_OnValueChanged);
        }

        private void UpdateErrorText(string text)
        {
            m_ErrorText.text = text;
        }

        protected override void OnUnbindFromModel(ITextInputWidget model)
        {
            m_InputField.onValueChanged.RemoveListener(InputField_OnValueChanged);
            base.OnUnbindFromModel(model);
        }

        private void UpdateInteractableState(bool isInteractable)
        {
            m_InputField.interactable = isInteractable;
        }

        private void UpdateCharacterMaskingState(bool isMasking)
        {
            if (isMasking)
                m_InputField.contentType = TMP_InputField.ContentType.Password;
            else
                m_InputField.contentType = m_CachedContentType;
            
            m_InputField.ForceLabelUpdate();
        }

        private void InputField_OnValueChanged(string value)
        {
            Model.TextProp.Set(value);
        }
    }
}