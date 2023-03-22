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
            m_TextInputWidgetView.Model = model.TextInputWidget;
            m_ShowPasswordToggle.Model = model.ShowPasswordToggleWidget;
        }
    }
}