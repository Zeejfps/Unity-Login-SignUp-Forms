using Login;
using UnityEngine;

public sealed class LoginSignUpWidgetView : WidgetView<ILoginSignUpWidget>
{
    [Header("Tabs")]
    [SerializeField] private TabWidgetView m_LoginFormTabWidgetView;
    [SerializeField] private TabWidgetView m_SignUpFormTabWidgetView;
    [Header("Content")]
    [SerializeField] private LoginFormWidgetView m_LoginFormWidgetView;
    [SerializeField] private SignUpFormWidgetView m_SignUpFormWidgetView;

    protected override void Awake()
    {
        base.Awake();
        
        Z.RegisterSingleton<ILoginService>(new BasicLoginService());
        Z.RegisterSingleton<IPopupService>(new BasicPopupService());
        
        Z.RegisterFactory<ILoginFormWidget>(Z.New<BasicLoginFormWidget>);
        Z.RegisterFactory<ISignUpFormWidget>(Z.New<BasicSignUpFormWidget>);
        
        Model = Z.New<BasicLoginSignUpWidget>();
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

public interface ISignUpFormWidget : IWidget
{
    
}