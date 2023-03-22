using UnityEngine;

namespace Login
{
    public sealed class UGUISignUpFormWidgetView : UGUIWidgetView<ISignUpFormWidget>
    {
        [SerializeField] private UGUITextInputWidgetView m_EmailInputWidgetView;
        [SerializeField] private UGUIPasswordInputWidgetView m_PasswordInputWidgetView;
        [SerializeField] private UGUIPasswordInputWidgetView m_ConfirmPasswordInputWidgetView;
        [SerializeField] private UGUIButtonWidgetView m_SubmitButtonWidgetView;
        
        protected override void OnBindToModel(ISignUpFormWidget model)
        {
            base.OnBindToModel(model);
            m_EmailInputWidgetView.Model = model.EmailInputWidget;
            m_PasswordInputWidgetView.Model = model.PasswordInputWidget;
            m_ConfirmPasswordInputWidgetView.Model = model.ConfirmPasswordInputWidget;
            m_SubmitButtonWidgetView.Model = model.SignUpButtonWidget;
        }
    }
}