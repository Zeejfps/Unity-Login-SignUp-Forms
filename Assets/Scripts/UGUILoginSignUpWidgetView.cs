using Login;
using UnityEngine;

public sealed class UGUILoginSignUpWidgetView : UGUIWidgetView<ILoginSignUpWidget>
{
    [SerializeField] private UGUITabWidgetView m_LoginFormTabWidgetView;
    [SerializeField] private UGUITabWidgetView m_SignUpFormTabWidgetView;
    [SerializeField] private UGUILoginFormWidgetView m_LoginFormWidgetView;
    [SerializeField] private UGUISignUpFormWidgetView m_SignUpFormWidgetView;

    protected override void Awake()
    {
        base.Awake();
        var loginSignUpWidget = Z.Get<ILoginSignUpWidget>();
        Model = loginSignUpWidget;
        Model.IsVisibleProp.Set(true);
    }

    protected override void OnBindToModel(ILoginSignUpWidget model)
    {
        base.OnBindToModel(model);
        m_LoginFormWidgetView.Model = model.LoginFormWidget;
        m_SignUpFormWidgetView.Model = model.SignUpFormWidget;
        m_LoginFormTabWidgetView.Model = model.LoginFormTabWidget;
        m_SignUpFormTabWidgetView.Model = model.SignUpFormTabWidget;
    }
}