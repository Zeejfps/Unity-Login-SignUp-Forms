using Login;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        Z.RegisterSingleton<ILoginManager, TestLoginManager>();
        Z.RegisterSingleton<IPopupManager, PopupManager>();
        Z.RegisterSingleton<ISignUpManager, TestSignUpManager>();
        
        Z.RegisterScoped<ISignUpConfirmation, TestSignUpConfirmation>();
        Z.RegisterScoped<ILoginFormWidget, LoginFormWidget>();
        Z.RegisterScoped<ISignUpFormWidget, SignUpFormWidget>();
        Z.RegisterScoped<ILoginSignUpWidget, LoginSignUpWidget>();
    }
}
