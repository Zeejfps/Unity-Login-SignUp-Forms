using Common.Widgets;
using SignUpForm;
using UnityEngine;
using YADBF.Unity;

namespace UGUI
{
    public sealed class UGUISignUpFormWidgetView : UGUIWidgetView<ISignUpFormWidget>
    {
        [SerializeField] private View<ITextFieldWidget> m_UsernameFieldWidgetView;
        [SerializeField] private UGUIPasswordFieldWidgetView m_ConfirmPasswordInputWidgetView;
        [SerializeField] private View<IListWidget> m_ListWidgetView;
        
        protected override void OnBindToModel(ISignUpFormWidget model)
        {
            base.OnBindToModel(model);
            m_UsernameFieldWidgetView.Model = model.UsernameFieldWidget;
            m_ConfirmPasswordInputWidgetView.Model = model.ConfirmPasswordFieldWidget;
            m_ListWidgetView.Model = model.PasswordRequirementsListWidget;
        }
    }
}