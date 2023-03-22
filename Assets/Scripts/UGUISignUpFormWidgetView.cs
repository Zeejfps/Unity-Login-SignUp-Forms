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
            Bind(model.EmailInputWidgetProp, m_EmailInputWidgetView);
            Bind(model.PasswordInputWidgetProp, m_PasswordInputWidgetView);
            Bind(model.ConfirmPasswordInputWidgetProp, m_ConfirmPasswordInputWidgetView);
            Bind(model.SignUpButtonWidgetProp, m_SubmitButtonWidgetView);
        }
    }
}