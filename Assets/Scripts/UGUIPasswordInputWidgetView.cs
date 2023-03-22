using UnityEngine;

namespace Login
{
    public sealed class UGUIPasswordInputWidgetView : UGUIWidgetView<IPasswordInputWidget>
    {
        [SerializeField] private UGUITextInputWidgetView m_TextInputWidgetView;
        [SerializeField] private UGUIToggleWidgetView m_ShowPasswordToggle;

        private bool m_IsShowingPassword;
        
        protected override void OnBindToModel(IPasswordInputWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.TextInputWidgetProp, m_TextInputWidgetView);
            Bind(model.ShowPasswordToggleWidgetProp, m_ShowPasswordToggle);
        }
    }
}