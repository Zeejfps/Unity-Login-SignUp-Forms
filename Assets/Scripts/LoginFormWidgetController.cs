using System;
using UnityEngine;
using YADBF;

internal sealed class LoginFormWidgetController : ILoginFormWidgetController
{
    public string Email
    {
        get => EmailInputWidget.TextProp.Value;
        set => EmailInputWidget.TextProp.Value = value;
    }

    public string Password
    {
        get => PasswordInputWidget.TextProp.Value;
        set => PasswordInputWidget.TextProp.Value = value;
    }

    private bool m_IsLoading;
    public bool IsLoading
    {
        get => m_IsLoading;
        set
        {
            m_IsLoading = value;
            OnIsLoadingStateChanged();
        }
    }

    public bool IsRememberMeChecked
    {
        get => RememberMeToggleWidget.IsOnProp.Value;
        set => RememberMeToggleWidget.IsOnProp.Value = value;
    }
    
    public bool IsEmailValid { get; private set; }
    public bool IsPasswordValid { get; private set; }
    

    private ITextInputWidget EmailInputWidget => EmailFieldWidget.TextInputWidget;
    private ITextFieldWidget EmailFieldWidget => LoginFormWidget.EmailFieldWidget;
    private ITextInputWidget PasswordInputWidget => PasswordFieldWidget.TextInputWidget;
    private IPasswordFieldWidget PasswordFieldWidget => LoginFormWidget.PasswordFieldWidget;
    private IButtonWidget SubmitButtonWidget => LoginFormWidget.SubmitButtonWidget;
    private IToggleWidget RememberMeToggleWidget => LoginFormWidget.RememberMeToggleWidget;
    
    private ILoginService LoginService { get; }
    private IEmailValidator EmailValidator { get; }
    private ILoginFormWidget LoginFormWidget { get; }

    public LoginFormWidgetController(ILoginService loginService, IEmailValidator emailValidator, ILoginFormWidget loginFormWidget)
    {
        LoginService = loginService;
        EmailValidator = emailValidator;
        LoginFormWidget = loginFormWidget;
        SubmitButtonWidget.ActionProp.Set(SubmitFormAsync);
        
        IsLoading = false;
        IsRememberMeChecked = true;
        
        EmailInputWidget.TextProp.ValueChanged += EmailInputWidget_TextProp_OnValueChanged;
        PasswordInputWidget.TextProp.ValueChanged += PasswordInputWidget_TextProp_OnValueChanged;

        UpdateSubmitButtonInteractionState();
    }
    
    public void Dispose()
    {
        EmailInputWidget.TextProp.ValueChanged -= EmailInputWidget_TextProp_OnValueChanged;
        PasswordInputWidget.TextProp.ValueChanged -= PasswordInputWidget_TextProp_OnValueChanged;
    }

    private void EmailInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        ValidateEmail();
    }

    private void PasswordInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        ValidatePassword();
    }

    private void ValidateEmail()
    {
        var isEmailValid = true;
        var validationResult = EmailValidator.Validate(Email);
        if (validationResult == EmailValidationStatus.Empty)
        {
            isEmailValid = false;
            EmailFieldWidget.ErrorTextProp.Set("Email is required");
        }
        else if (validationResult == EmailValidationStatus.Invalid)
        {
            isEmailValid = false;
            EmailFieldWidget.ErrorTextProp.Set("Invalid email");
        }
        else
        {
            EmailFieldWidget.ErrorTextProp.Set(string.Empty);
        }

        IsEmailValid = isEmailValid;
        UpdateSubmitButtonInteractionState();
    }

    private void ValidatePassword()
    {
        IsPasswordValid = !string.IsNullOrWhiteSpace(Password);
        if (!IsPasswordValid)
        {
            PasswordFieldWidget.ErrorTextProperty.Set("Password is required");
        }
        else
        {
            PasswordFieldWidget.ErrorTextProperty.Set(string.Empty);
        }
        
        UpdateSubmitButtonInteractionState();
    }

    private void OnIsLoadingStateChanged()
    {
        var isLoading = IsLoading;
        EmailInputWidget.IsInteractableProperty.Set(!isLoading);
        PasswordInputWidget.IsInteractableProperty.Set(!isLoading);
        RememberMeToggleWidget.IsInteractableProperty.Set(!isLoading);
        SubmitButtonWidget.IsLoadingProp.Set(isLoading);
        UpdateSubmitButtonInteractionState();
    }

    private void UpdateSubmitButtonInteractionState()
    {
        SubmitButtonWidget.IsInteractableProp.Set(!IsLoading && IsEmailValid && IsPasswordValid);
    }

    private async void SubmitFormAsync()
    {
        try
        {
            var email = Email;
            var password = Password;

            IsLoading = true;
            await LoginService.LoginAsync(email, password);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            IsLoading = false;
        }
    }
}