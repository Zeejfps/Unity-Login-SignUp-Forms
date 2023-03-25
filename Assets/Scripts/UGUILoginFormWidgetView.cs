using Login;
using UnityEngine;
using YADBF.Unity;

public sealed class UGUILoginFormWidgetView : UGUIWidgetView<ILoginFormWidget>
{
    [SerializeField] private View<ITextFieldWidget> m_EmailFieldWidgetView;
    [SerializeField] private View<IPasswordFieldWidget> m_PasswordInputWidgetView;
    [SerializeField] private View<IButtonWidget> m_LoginButtonWidgetView;

    protected override void OnBindToModel(ILoginFormWidget model)
    {
        base.OnBindToModel(model);
        m_EmailFieldWidgetView.Model = model.EmailFieldWidget;
        m_PasswordInputWidgetView.Model = model.PasswordFieldWidget;
        m_LoginButtonWidgetView.Model = model.LoginButtonWidget;
    }
}