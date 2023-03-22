using UnityEngine;

namespace Login
{
    public sealed class SignUpFormWidgetView : WidgetView<ISignUpFormWidget>
    {
        [SerializeField] private TextInputWidgetView m_EmailInputWidgetView;
        [SerializeField] private TextInputWidgetView m_PasswordInputWidgetView;
        [SerializeField] private TextInputWidgetView m_ConfirmPasswordInputWidgetView;
        
        protected override void OnBindToModel(ISignUpFormWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.EmailInputWidgetProp, m_EmailInputWidgetView);
            Bind(model.PasswordInputWidgetProp, m_PasswordInputWidgetView);
            Bind(model.ConfirmPasswordInputWidgetProp, m_ConfirmPasswordInputWidgetView);
        }
    }
}