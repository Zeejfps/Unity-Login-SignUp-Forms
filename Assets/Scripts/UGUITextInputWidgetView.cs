using TMPro;
using UnityEngine;

namespace Login
{
    public sealed class UGUITextInputWidgetView : UGUIWidgetView<ITextInputWidget>
    {
        [SerializeField] private TMP_InputField m_InputField;

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
            Bind(model.IsInteractableProp, UpdateInteractableState);
            Bind(model.IsMaskingCharacters, UpdateCharacterMaskingState);
            m_InputField.onValueChanged.AddListener(InputField_OnValueChanged);
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