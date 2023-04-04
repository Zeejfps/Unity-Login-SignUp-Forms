using Login;
using UnityEngine;

public sealed class UGUILoginSignUpWidgetView : UGUIWidgetView<ILoginSignUpPageWidget>
{
    [SerializeField] private UGUITabWidgetView m_LoginFormTabWidgetView;
    [SerializeField] private UGUITabWidgetView m_SignUpFormTabWidgetView;
    [SerializeField] private UGUILoginFormWidgetView m_LoginFormWidgetView;
    [SerializeField] private UGUISignUpFormWidgetView m_SignUpFormWidgetView;

    protected override void Awake()
    {
        base.Awake();

        var loginService = Z.Get<ILoginService>();
        var signUpService = Z.Get<ISignUpService>();
        var loginSignUpPageWidget = Z.Get<ILoginSignUpPageWidget>();

        var controller = new LoginSignUpPageWidgetController(loginService, signUpService, loginSignUpPageWidget);
        
        Model = loginSignUpPageWidget;
        Model.IsVisibleProp.Set(true);
    }

    protected override void OnBindToModel(ILoginSignUpPageWidget model)
    {
        base.OnBindToModel(model);
        m_LoginFormWidgetView.Model = model.LoginFormWidget;
        m_SignUpFormWidgetView.Model = model.SignUpFormWidget;
        m_LoginFormTabWidgetView.Model = model.LoginFormTabWidget;
        m_SignUpFormTabWidgetView.Model = model.SignUpFormTabWidget;
    }
}