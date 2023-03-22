using TMPro;
using UnityEngine;

namespace Login
{
    public sealed class UGUIPasswordInputWidgetView : UGUIWidgetView<IPasswordInputWidget>
    {
        [SerializeField] private TMP_InputField m_InputField;
        [SerializeField] private UGUIToggleWidgetView m_ShowPasswordToggle;

        private bool m_IsShowingPassword;
        
        protected override void OnBindToModel(IPasswordInputWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.TextProp, m_InputField.SetTextWithoutNotify);
            Bind(model.IsInteractableProp, value =>
            {
                m_InputField.interactable = value;
            });
            Bind(model.IsShowingPasswordProp, UpdateShowPasswordState);
            Bind(model.ShowPasswordToggleWidgetProp, m_ShowPasswordToggle);
            
            m_InputField.onValueChanged.AddListener(InputField_OnValueChanged);
        }

        protected override void OnUnbindFromModel(IPasswordInputWidget model)
        {
            m_InputField.onValueChanged.RemoveListener(InputField_OnValueChanged);
            base.OnUnbindFromModel(model);
        }

        private void UpdateShowPasswordState(bool isShowingPassword)
        {
            if (isShowingPassword)
                m_InputField.contentType = TMP_InputField.ContentType.Standard;
            else
                m_InputField.contentType = TMP_InputField.ContentType.Password;
            
            m_InputField.ForceLabelUpdate();
        }

        private void InputField_OnValueChanged(string value)
        {
            Model.TextProp.Set(value);
        }
    }
}