using System;
using UnityEngine;
using UnityEngine.UI;

namespace YADBF.Unity.BindingsUGUI
{
    public sealed class ActionToButtonBinding : PropertyBinding<Action>
    {
        [SerializeField] private Button m_Button;

        protected override void OnBound(Action currValue)
        {
            base.OnBound(currValue);
            m_Button.onClick.AddListener(Button_OnClicked);
        }

        protected override void OnUnbound()
        {
            m_Button.onClick.RemoveListener(Button_OnClicked);
            base.OnUnbound();
        }

        protected override void Reset()
        {
            base.Reset();
            m_Button = GetComponent<Button>();
        }

        protected override void OnPropertyValueChanged(Action prevValue, Action currValue)
        {
            m_Button.interactable = currValue != null;
        }

        private void Button_OnClicked()
        {
            PropertyValue.Invoke();
        }
    }
}