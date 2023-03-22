using Login;
using UnityEngine;

public sealed class UGUILoginSignUpWidgetView : UGUIWidgetView<ILoginSignUpWidget>
{
    [Header("Tabs")]
    [SerializeField] private UGUITabWidgetView m_LoginFormTabWidgetView;
    [SerializeField] private UGUITabWidgetView m_SignUpFormTabWidgetView;
    [Header("Content")]
    [SerializeField] private UGUILoginFormWidgetView m_LoginFormWidgetView;
    [SerializeField] private UGUISignUpFormWidgetView m_SignUpFormWidgetView;

    protected override void Awake()
    {
        base.Awake();
        Model = Z.Get<ILoginSignUpWidget>();
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