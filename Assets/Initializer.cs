using Login;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class Initializer : MonoBehaviour
{
    private void Awake()
    {
        Z.RegisterSingleton<ILoginManager, TestLoginManager>();
        Z.RegisterSingleton<IPopupService, BasicPopupService>();
        Z.RegisterSingleton<ISignUpManager, TestSignUpManager>();

        Z.RegisterScoped<ILoginFormWidget, LoginFormWidget>();
        Z.RegisterScoped<ISignUpFormWidget, BasicSignUpFormWidget>();
        Z.RegisterScoped<ILoginSignUpWidget, BasicLoginSignUpWidget>();
    }
}
