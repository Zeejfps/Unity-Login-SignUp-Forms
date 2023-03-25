using Login;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        Z.RegisterSingleton<ILoginForm, TestLoginForm>();
        Z.RegisterSingleton<IPopupManager, PopupManager>();
        Z.RegisterSingleton<ISignUpForm, TestSignUpForm>();
        
        Z.RegisterScoped<ILoginFormWidget, LoginFormWidget>();
        Z.RegisterScoped<ISignUpFormWidget, SignUpFormWidget>();
        Z.RegisterScoped<ILoginSignUpWidget, LoginSignUpWidget>();
    }
}
