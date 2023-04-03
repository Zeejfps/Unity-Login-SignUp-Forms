using System;
using System.Linq;
using Tests;
using UnityEngine;
using YADBF;

public sealed class SignUpFormWidgetController : ISignUpFormWidgetController
{
    public event Action Submitted;
    public event Action EmailChanged;
    public event Action UsernameChanged;
    public event Action PasswordChanged;
    public event Action ConfirmPasswordChanged;

    public ISignUpFormWidget SignUpFormWidget { get; }

    private IPasswordRequirement[] PasswordRequirements { get; }
    private IEmailValidator EmailValidator { get; }
    private ISignUpService SignUpService { get; }
        
    public string Email => EmailInputWidget.TextProp.Value;
    public string Username => UsernameInputWidget.TextProp.Value;
    public string Password => PasswordInputWidget.TextProp.Value;
    public string ConfirmPassword
    {
        get => ConfirmPasswordInputWidget.TextProp.Value;

        set => ConfirmPasswordInputWidget.TextProp.Value = value;
    }

    private bool m_IsLoading;
    public bool IsLoading
    {
        get => m_IsLoading;
        set
        {
            m_IsLoading = value;
            OnLoadingStateChanged();
        }
    }

    private ITextInputWidget EmailInputWidget => EmailFieldWidget.TextInputWidget;
    private ITextFieldWidget EmailFieldWidget => SignUpFormWidget.EmailFieldWidget;
    private ITextInputWidget UsernameInputWidget => UsernameFieldWidget.TextInputWidget;
    private ITextFieldWidget UsernameFieldWidget => SignUpFormWidget.UsernameFieldWidget;
    private ITextInputWidget PasswordInputWidget => PasswordFieldWidget.TextInputWidget;
    private IPasswordFieldWidget PasswordFieldWidget => SignUpFormWidget.PasswordFieldWidget;
    private ITextInputWidget ConfirmPasswordInputWidget => ConfirmPasswordFieldWidget.TextInputWidget;
    private IPasswordFieldWidget ConfirmPasswordFieldWidget => SignUpFormWidget.ConfirmPasswordFieldWidget;
    private IButtonWidget SubmitButtonWidget => SignUpFormWidget.SignUpButtonWidget;
        
    private bool IsEmailValid { get; set; }
    private bool IsUsernameValid { get; set; }
    private bool IsPasswordValid { get; set; }
    private bool IsConfirmPasswordValid { get; set; }

    private IStateMachine StateMachine { get; }

    public SignUpFormWidgetController(ISignUpService signUpService, ISignUpFormWidget signUpFormWidget)
    {
        SignUpService = signUpService;
        SignUpFormWidget = signUpFormWidget;
            
        EmailValidator = new RegexEmailValidator();
        StateMachine = new SimpleStateMachine();
        PasswordRequirements = SignUpService.GetPasswordRequirements().ToArray();

        SubmitButtonWidget.ActionProp.Set(SubmitForm);
        EmailInputWidget.TextProp.ValueChanged += EmailInputWidget_TextProp_OnValueChanged;
        UsernameInputWidget.TextProp.ValueChanged += UsernameInputWidget_TextProp_OnValueChanged;
        PasswordInputWidget.TextProp.ValueChanged += PasswordInputWidget_TextProp_OnValueChanged;
        ConfirmPasswordInputWidget.TextProp.ValueChanged += ConfirmPasswordInputWidget_TextProp_OnValueChanged;
            
        StateMachine.State = new SignUpFormWidgetControllerDefaultState(this);
    }

    private void EmailInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        EmailChanged?.Invoke();
    }

    private void UsernameInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        UsernameChanged?.Invoke();
    }
        
    private void PasswordInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        PasswordChanged?.Invoke();
    }
        
    private void ConfirmPasswordInputWidget_TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        ConfirmPasswordChanged?.Invoke();
    }

    public void ValidateEmail()
    {
        var email = Email;
        var isEmailValid = true;
        if (string.IsNullOrWhiteSpace(email))
        {
            EmailFieldWidget.ErrorTextProp.Set("Email is required");
            isEmailValid = false;
        }
        else if (EmailValidator.Validate(email) == EmailValidationStatus.Invalid)
        {
            EmailFieldWidget.ErrorTextProp.Set("Email is invalid");
            isEmailValid = false;
        }
        else
        {
            EmailFieldWidget.ErrorTextProp.Set(string.Empty);
        }

        IsEmailValid = isEmailValid;
        UpdateSubmitButtonState();
    }

    public void ValidateUsername()
    {
        var username = Username;
        var isUsernameValid = true;

        if (string.IsNullOrWhiteSpace(username))
        {
            UsernameFieldWidget.ErrorTextProp.Set("Username is required");
            isUsernameValid = false;
        }
        else
        {
            UsernameFieldWidget.ErrorTextProp.Set(string.Empty);
        }

        IsUsernameValid = isUsernameValid;
    }

    public void ValidatePassword()
    {
        var password = Password;
        var isPasswordValid = true;
        var allPasswordRequirementsMet = true;
            
        foreach (var passwordRequirement in PasswordRequirements)
        {
            if (!passwordRequirement.Validate(Password))
            {
                allPasswordRequirementsMet = false;
                break;
            }
        }
            
        if (string.IsNullOrWhiteSpace(password))
        {
            PasswordFieldWidget.ErrorTextProperty.Set("Password is required");
            isPasswordValid = false;
        }
        else if (!allPasswordRequirementsMet)
        {
            PasswordFieldWidget.ErrorTextProperty.Set("Not all requirements met");
            isPasswordValid = false;
        }
        else
        {
            PasswordFieldWidget.ErrorTextProperty.Set(string.Empty);
        }

        IsPasswordValid = isPasswordValid;
        UpdateSubmitButtonState();
    }

    public void ValidateConfirmPassword()
    {
        var password = Password;
        var confirmPassword = ConfirmPassword;
        var isConfirmPasswordValid = true;

        if (string.IsNullOrWhiteSpace(confirmPassword))
        {
            ConfirmPasswordFieldWidget.ErrorTextProperty.Set("Please confirm password");
            isConfirmPasswordValid = false;
        }
        else if (confirmPassword != password)
        {
            ConfirmPasswordFieldWidget.ErrorTextProperty.Set("Passwords do not match");
            isConfirmPasswordValid = false;
        }
        else
        {
            ConfirmPasswordFieldWidget.ErrorTextProperty.Set(string.Empty);
        }

        IsConfirmPasswordValid = isConfirmPasswordValid;
        UpdateSubmitButtonState();
    }

    private void OnLoadingStateChanged()
    {
        var isLoading = IsLoading;
        EmailInputWidget.IsInteractableProperty.Set(!isLoading);
        UsernameInputWidget.IsInteractableProperty.Set(!isLoading);
        PasswordInputWidget.IsInteractableProperty.Set(!isLoading);
        ConfirmPasswordInputWidget.IsInteractableProperty.Set(!isLoading);
        SubmitButtonWidget.IsLoadingProp.Set(isLoading);
        UpdateSubmitButtonState();
    }

    private void UpdateSubmitButtonState()
    {
        SubmitButtonWidget.IsInteractableProp.Set(IsEmailValid && IsPasswordValid && IsConfirmPasswordValid && !IsLoading);

    }

    private async void SubmitForm()
    {
        try
        {
            StateMachine.State = new SignUpFormWidgetControllerSubmittingFormState(this);
            await SignUpService.SignUpAsync(Email, Username, Password);
            Submitted?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            StateMachine.State = new SignUpFormWidgetControllerDefaultState(this);
        }
    }
}