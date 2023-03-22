using UnityEngine;

namespace Login
{
    public sealed class UGUIPasswordInputWidgetView : UGUIWidgetView<IPasswordFieldWidget>
    {
        [SerializeField] private UGUITextInputWidgetView m_TextInputWidgetView;
        [SerializeField] private UGUIToggleWidgetView m_ShowPasswordToggle;

        private bool m_IsShowingPassword;
        
        protected override void OnBindToModel(IPasswordFieldWidget model)
        {
            base.OnBindToModel(model);
            m_TextInputWidgetView.Model = model.TextInputWidget;
            m_ShowPasswordToggle.Model = model.ShowPasswordToggleWidget;
        }
    }
}