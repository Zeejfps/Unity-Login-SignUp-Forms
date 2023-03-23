using System;
using System.Threading.Tasks;
using Login;
using UnityEngine;
using YADBF;

internal sealed class TestLoginManager : ILoginManager
{
    public ObservableProperty<string> EmailProp { get; } = new();
    public ObservableProperty<string> PasswordProp { get; } = new();
    public ObservableProperty<bool> IsLoadingProp { get; } = new();
    public ObservableProperty<Action> LoginActionProp { get; } = new();

    private IPopupService PopupService { get; }

    public TestLoginManager(IPopupService popupService)
    {
        PopupService = popupService;
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
            LoginActionProp.Set(null);
        }
        else
        {
            LoginActionProp.Set(LoginAsync);
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
            await PopupService.ShowInfoPopupAsync("Invalid Credentials", "Email and/or Password was incorrect");
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