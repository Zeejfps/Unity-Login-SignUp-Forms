using Login;
using Tests;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        Z.RegisterSingleton<ISignUpService, TestSignUpService>();
        Z.RegisterSingleton<IPopupManager, PopupManager>();
        
        Z.RegisterScoped<ILoginForm, TestLoginForm>();
        Z.RegisterScoped<ISignUpFormController, SignUpFormController>();
        Z.RegisterScoped<ISignUpConfirmationForm, TestSignUpConfirmationForm>();
        Z.RegisterScoped<ILoginSignUpWidget, LoginSignUpWidget>();
    }
}
