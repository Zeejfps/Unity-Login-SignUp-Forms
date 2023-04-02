using UnityEngine;
using YADBF.Unity;

namespace Login
{
    public sealed class UGUISignUpFormWidgetView : UGUIWidgetView<ISignUpFormWidget>
    {
        [SerializeField] private View<ITextFieldWidget> m_EmailFieldWidgetView;
        [SerializeField] private View<ITextFieldWidget> m_UsernameFieldWidgetView;
        [SerializeField] private UGUIPasswordFieldWidgetView m_PasswordInputWidgetView;
        [SerializeField] private UGUIPasswordFieldWidgetView m_ConfirmPasswordInputWidgetView;
        [SerializeField] private UGUIButtonWidgetView m_SubmitButtonWidgetView;
        [SerializeField] private int m_InsertionIndex;
        [SerializeField] private View<IPasswordRequirementWidget> m_PasswordRequirementWidgetViewPrefab;
        
        protected override void OnBindToModel(ISignUpFormWidget model)
        {
            base.OnBindToModel(model);
            m_EmailFieldWidgetView.Model = model.EmailFieldWidget;
            m_UsernameFieldWidgetView.Model = model.UsernameFieldWidget;
            m_PasswordInputWidgetView.Model = model.PasswordFieldWidget;
            m_ConfirmPasswordInputWidgetView.Model = model.ConfirmPasswordFieldWidget;
            m_SubmitButtonWidgetView.Model = model.SignUpButtonWidget;

            var insertionIndex = m_InsertionIndex;
            for (var i = 0; i < model.PasswordRequirementWidgets.Length; i++)
            {
                var siblingIndex = insertionIndex + i;
                var view = Instantiate(m_PasswordRequirementWidgetViewPrefab, transform);
                view.transform.SetSiblingIndex(siblingIndex);
                view.Model = model.PasswordRequirementWidgets[i];
            }
        }
    }
}