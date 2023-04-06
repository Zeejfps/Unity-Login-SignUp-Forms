using Login;
using SignUpConfirmationForm;
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
        
        Z.RegisterScoped<ILoginFormWidgetController, LoginFormWidgetController>();
        Z.RegisterScoped<ISignUpFormWidgetController, SignUpFormWidgetController>();
        Z.RegisterScoped<ISignUpConfirmationFormWidgetController, SignUpConfirmationFormWidgetController>();
        Z.RegisterScoped<ILoginSignUpPageWidget, LoginSignUpPageWidget>();
        Z.RegisterScoped<ILoginFormWidget, LoginFormWidget>();
        Z.RegisterScoped<ISignUpFormWidget, SignUpFormWidget>();
    }
}
