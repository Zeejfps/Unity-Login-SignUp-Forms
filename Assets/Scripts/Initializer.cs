using Login;
using LoginForm;
using LoginSignUpPage;
using Services;
using SignUpConfirmationForm;
using SignUpForm;
using Tests;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        Z.RegisterSingleton<ISignUpService, TestSignUpService>();
        Z.RegisterSingleton<ILoginService, TestLoginService>();
        Z.RegisterSingleton<IPopupManager, PopupManager>();
        
        Z.RegisterScoped<ILoginFormWidgetPresenter, LoginFormWidgetPresenter>();
        Z.RegisterScoped<ISignUpFormWidgetPresenter, SignUpFormWidgetPresenter>();
        Z.RegisterScoped<ISignUpConfirmationPopupWidgetPresenter, SignUpConfirmationPopupWidgetPresenter>();
        Z.RegisterScoped<ILoginSignUpPageWidget, LoginSignUpPageWidget>();
        Z.RegisterScoped<ILoginFormWidget, LoginFormWidget>();
        Z.RegisterScoped<ISignUpFormWidget, SignUpFormWidget>();
    }
}
