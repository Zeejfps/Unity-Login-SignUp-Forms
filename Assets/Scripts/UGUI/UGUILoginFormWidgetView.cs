using Common.Widgets;
using LoginForm;
using UnityEngine;
using YADBF.Unity;

namespace UGUI
{
    public sealed class UGUILoginFormWidgetView : UGUIWidgetView<ILoginFormWidget>
    {
        [SerializeField] private View<ITextFieldWidget> m_EmailFieldWidgetView;
        [SerializeField] private View<IPasswordFieldWidget> m_PasswordInputWidgetView;
        [SerializeField] private View<IButtonWidget> m_LoginButtonWidgetView;
        [SerializeField] private View<IToggleWidget> m_RememberMeToggleWidgetView;

        protected override void OnBindToModel(ILoginFormWidget model)
        {
            base.OnBindToModel(model);
            m_EmailFieldWidgetView.Model = model.EmailFieldWidget;
            m_PasswordInputWidgetView.Model = model.PasswordFieldWidget;
            m_LoginButtonWidgetView.Model = model.SubmitButtonWidget;
            m_RememberMeToggleWidgetView.Model = model.RememberMeToggleWidget;
        }
    }
}