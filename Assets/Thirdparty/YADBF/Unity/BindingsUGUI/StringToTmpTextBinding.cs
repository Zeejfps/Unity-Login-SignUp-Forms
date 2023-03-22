using TMPro;
using UnityEngine;

namespace YADBF.Unity.BindingsUGUI
{
    public sealed class StringToTmpTextBinding : PropertyBinding<string>
    {
        [SerializeField] private TMP_Text m_Text;

        protected override void Reset()
        {
            base.Reset();
            m_Text = GetComponent<TMP_Text>();
        }

        protected override void OnPropertyValueChanged(string prevValue, string currValue)
        {
            m_Text.text = currValue;
        }
    }
}
