using Common.Widgets;
using LoginForm;
using UnityEngine;
using YADBF.Unity;

namespace UGUI
{
    public sealed class UGUILoginFormWidgetView : UGUIWidgetView<ILoginFormWidget>
    {
        [SerializeField] private View<IPasswordFieldWidget> m_PasswordInputWidgetView;
        [SerializeField] private View<IToggleWidget> m_RememberMeToggleWidgetView;

        protected override void OnBindToModel(ILoginFormWidget model)
        {
            base.OnBindToModel(model);
            m_PasswordInputWidgetView.Model = model.PasswordFieldWidget;
            m_RememberMeToggleWidgetView.Model = model.RememberMeToggleWidget;
        }
    }
}