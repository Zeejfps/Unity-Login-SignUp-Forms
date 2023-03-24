using Login;
using UnityEngine;
using YADBF.Unity;

public sealed class UGUILoginFormWidgetView : UGUIWidgetView<ILoginFormWidget>
{
    [SerializeField] private View<ITextInputWidget> m_EmailInputWidgetView;
    [SerializeField] private View<IPasswordFieldWidget> m_PasswordInputWidgetView;
    [SerializeField] private View<IButtonWidget> m_LoginButtonWidgetView;

    protected override void OnBindToModel(ILoginFormWidget model)
    {
        base.OnBindToModel(model);
        m_EmailInputWidgetView.Model = model.EmailInputWidget;
        m_PasswordInputWidgetView.Model = model.PasswordInputWidget;
        m_LoginButtonWidgetView.Model = model.LoginButtonWidget;
    }
}