using System;
using System.Threading.Tasks;
using Login;
using UnityEngine;
using YADBF;

internal sealed class TestLoginForm : ILoginForm
{
    public ObservableProperty<string> EmailProp { get; } = new();
    public ObservableProperty<string> PasswordProp { get; } = new();
    public ObservableProperty<bool> IsLoadingProp { get; } = new();
    public ObservableProperty<Action> SubmitActionProp { get; } = new();

    private IPopupManager PopupManager { get; }

    public TestLoginForm(IPopupManager popupManager)
    {
        PopupManager = popupManager;
        EmailProp.ValueChanged += EmailProp_OnValueChanged;
        PasswordProp.ValueChanged += PasswordProp_OnValueChanged;
        UpdateState();
    }

    private void EmailProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        UpdateState();
    }

    private void PasswordProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        UpdateState();
    }

    private void UpdateState()
    {
        var email = EmailProp.Value;
        var password = PasswordProp.Value;

        if (string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(password))
        {
            SubmitActionProp.Set(null);
        }
        else
        {
            SubmitActionProp.Set(LoginAsync);
        }
    }

    private async void LoginAsync()
    {
        try
        {
            var email = EmailProp.Value;
            var password = PasswordProp.Value;

            IsLoadingProp.Set(true);

            await Task.Delay(2000);
            await PopupManager.ShowInfoPopupAsync("Invalid Credentials", "Email and/or Password was incorrect");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            IsLoadingProp.Set(false);
        }
    }
}