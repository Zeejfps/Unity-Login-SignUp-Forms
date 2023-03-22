using TMPro;
using UnityEngine;

namespace YADBF.Unity.BindingsUGUI
{
    public sealed class StringToTmpInputFieldBinding : PropertyBinding<string>
    {
        [SerializeField] private TMP_InputField m_InputField;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            m_InputField.onValueChanged.AddListener(InputField_OnValueChanged);
        }

        protected override void OnDisable()
        {
            m_InputField.onValueChanged.RemoveListener(InputField_OnValueChanged);
            base.OnDisable();
        }

        protected override void Reset()
        {
            Debug.Log("Reset");
            base.Reset();
            m_InputField = GetComponent<TMP_InputField>();
        }

        protected override void OnPropertyValueChanged(string prevValue, string currValue)
        {
            m_InputField.SetTextWithoutNotify(currValue);
        }

        private void InputField_OnValueChanged(string value)
        {
            PropertyValue = value;
        }
    }
}