using Login;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        Z.RegisterSingleton<ILoginFlow, TestLoginFlow>();
        Z.RegisterSingleton<IPopupManager, PopupManager>();
        Z.RegisterSingleton<ISignUpFlow, TestSignUpFlow>();
        
        Z.RegisterScoped<ILoginFormWidget, LoginFormWidget>();
        Z.RegisterScoped<ISignUpFormWidget, SignUpFormWidget>();
        Z.RegisterScoped<ILoginSignUpWidget, LoginSignUpWidget>();
    }
}
