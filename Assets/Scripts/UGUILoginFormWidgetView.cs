using UnityEngine;
using YADBF.Unity;

namespace Login
{
    public sealed class UGUILoginFormWidgetView : UGUIWidgetView<ILoginFormWidget>
    {
        [SerializeField] private View<ITextInputWidget> m_EmailInputWidgetView;
        [SerializeField] private UGUIPasswordInputWidgetView m_PasswordInputWidgetView;
        [SerializeField] private UGUIButtonWidgetView m_LoginButtonWidgetView;

        protected override void OnBindToModel(ILoginFormWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.IsVisibleProp, gameObject.SetActive);

            m_EmailInputWidgetView.Model = model.EmailInputWidget;
            m_PasswordInputWidgetView.Model = model.PasswordInputWidget;
            m_LoginButtonWidgetView.Model = model.LoginButtonWidget;
        }
    }
}