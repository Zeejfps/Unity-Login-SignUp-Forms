using TMPro;
using UnityEngine;
using YADBF.Unity;

namespace Login
{
    public sealed class TextInputWidgetView : WidgetView<ITextInputWidget>
    {
        [SerializeField] private TMP_InputField m_InputField;

        protected override void OnBindToModel(ITextInputWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.TextProp, m_InputField.SetTextWithoutNotify);
            Bind(model.IsInteractableProp, value => m_InputField.interactable = value);
            m_InputField.onValueChanged.AddListener(InputField_OnValueChanged);
        }

        protected override void OnUnbindFromModel(ITextInputWidget model)
        {
            m_InputField.onValueChanged.RemoveListener(InputField_OnValueChanged);
            base.OnUnbindFromModel(model);
        }

        private void InputField_OnValueChanged(string value)
        {
            Model.TextProp.Set(value);
        }
    }
}