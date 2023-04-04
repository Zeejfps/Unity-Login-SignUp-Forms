using Login;
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
        Z.RegisterScoped<ISignUpConfirmationForm, TestSignUpConfirmationForm>();
        Z.RegisterScoped<ILoginSignUpPageWidget, LoginSignUpWidget>();
        Z.RegisterScoped<ILoginFormWidget, LoginFormWidget>();
        Z.RegisterScoped<ISignUpFormWidget, SignUpFormWidget>();
    }
}
