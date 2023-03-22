using UnityEngine;
using YADBF.Unity;

namespace Login
{
    public sealed class LoginFormWidgetView : WidgetView<ILoginFormWidget>
    {
        [SerializeField] private TextInputWidgetView m_EmailInputWidgetView;
        [SerializeField] private TextInputWidgetView m_PasswordInputWidgetView;

        protected override void OnBindToModel(ILoginFormWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.IsVisibleProp, gameObject.SetActive);
            Bind(model.EmailInputWidgetProp, m_EmailInputWidgetView);
            Bind(model.PasswordInputWidgetProp, m_PasswordInputWidgetView);
        }
    }
}