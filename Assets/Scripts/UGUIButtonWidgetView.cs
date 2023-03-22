using UnityEngine;
using UnityEngine.UI;

namespace Login
{
    public sealed class UGUIButtonWidgetView : UGUIWidgetView<IButtonWidget>
    {
        [SerializeField] private Button m_Button;

        protected override void OnBindToModel(IButtonWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.IsInteractable, value => m_Button.interactable = value);
            m_Button.onClick.AddListener(Button_OnClicked);
        }

        protected override void OnUnbindFromModel(IButtonWidget model)
        {
            m_Button.onClick.RemoveListener(Button_OnClicked);
            base.OnUnbindFromModel(model);
        }

        private void Button_OnClicked()
        {
            Model.ActionProp.Value?.Invoke();
        }
    }
}