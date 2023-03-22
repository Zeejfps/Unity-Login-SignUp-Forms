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

        Z.RegisterSingleton<ILoginService, BasicLoginService>();
        Z.RegisterSingleton<IPopupService, BasicPopupService>();

        Z.RegisterScoped<ILoginFormWidget, BasicLoginFormWidget>();
        Z.RegisterScoped<ISignUpFormWidget, BasicSignUpFormWidget>();
        Z.RegisterScoped<ILoginSignUpWidget, BasicLoginSignUpWidget>();

        Model = Z.Get<ILoginSignUpWidget>();
        Model.IsVisibleProp.Set(true);
    }

    protected override void OnBindToModel(ILoginSignUpWidget model)
    {
        base.OnBindToModel(model);
        Bind(model.LoginFormTabWidgetProp, m_LoginFormTabWidgetView);
        Bind(model.SignUpFormTabWidgetProp, m_SignUpFormTabWidgetView);
        Bind(model.LoginFormWidgetProp, m_LoginFormWidgetView);
        Bind(model.SignUpFormWidgetProp, m_SignUpFormWidgetView);
    }
}