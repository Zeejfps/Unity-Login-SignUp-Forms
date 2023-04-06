using System;
using Tests;
using UnityEngine;
using YADBF;

public sealed class SignUpFormWidgetController : ISignUpFormWidgetController
{
    public event Action FormSubmitted;
    public event Action EmailChanged;
    public event Action UsernameChanged;
    public event Action PasswordChanged;
    public event Action ConfirmPasswordChanged;
    
    public string Email => EmailInputWidget.TextProp.Value;
    public string Username => UsernameInputWidget.TextProp.Value;

    public string Password
    {
        get => PasswordInputWidget.TextProp.Value;
        set => PasswordInputWidget.TextProp.Value = value;
    }
    
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
        
    private ISignUpFormWidget SignUpFormWidget { get; }
    private IPasswordRequirement[] PasswordRequirements { get; }
    private IEmailValidator EmailValidator { get; }
    private IPasswordValidator PasswordValidator { get; }
    private ISignUpService SignUpService { get; }

    private bool IsEmailValid { get; set; }
    private bool IsUsernameValid { get; set; }
    private bool IsPasswordValid { get; set; }
    private bool IsConfirmPasswordValid { get; set; }

    private IStateMachine StateMachine { get; }
    private IFocusController FocusController { get; }

    public SignUpFormWidgetController(
        ISignUpService signUpService, 
        IEmailValidator emailValidator, 
        IPasswordValidator passwordValidator,
        ISignUpFormWidget signUpFormWidget)
    {
        SignUpService = signUpService;
        SignUpFormWidget = signUpFormWidget;
            
        EmailValidator = emailValidator;
        PasswordValidator = passwordValidator;
        PasswordRequirements = PasswordValidator.PasswordRequirements;
        StateMachine = new SimpleStateMachine();

        SubmitButtonWidget.ActionProp.Set(SubmitForm);
        SignUpFormWidget.IsVisibleProp.ValueChanged += SignUpFormWidget_IsVisibleProp_OnValueChanged;
        EmailInputWidget.TextProp.ValueChanged += EmailInputWidget_TextProp_OnValueChanged;
        UsernameInputWidget.TextProp.ValueChanged += UsernameInputWidget_TextProp_OnValueChanged;
        PasswordInputWidget.TextProp.ValueChanged += PasswordInputWidget_TextProp_OnValueChanged;
        ConfirmPasswordInputWidget.TextProp.ValueChanged += ConfirmPasswordInputWidget_TextProp_OnValueChanged;

        foreach (var passwordRequirement in PasswordValidator.PasswordRequirements)
            SignUpFormWidget.PasswordRequirementsListWidget.Add(new SignUpFormPasswordRequirementWidget(PasswordInputWidget, passwordRequirement));

        StateMachine.State = new SignUpFormWidgetControllerDefaultState(this);

        FocusController = new FocusController
        {
            CanCycle = true
        };
        FocusController.Add(EmailInputWidget);
        FocusController.Add(UsernameInputWidget);
        FocusController.Add(PasswordInputWidget);
        FocusController.Add(ConfirmPasswordInputWidget);
    }

    public void Dispose()
    {
        FocusController.Dispose();
        SignUpFormWidget.IsVisibleProp.ValueChanged -= SignUpFormWidget_IsVisibleProp_OnValueChanged;
        EmailInputWidget.TextProp.ValueChanged -= EmailInputWidget_TextProp_OnValueChanged;
        UsernameInputWidget.TextProp.ValueChanged -= UsernameInputWidget_TextProp_OnValueChanged;
        PasswordInputWidget.TextProp.ValueChanged -= PasswordInputWidget_TextProp_OnValueChanged;
        ConfirmPasswordInputWidget.TextProp.ValueChanged -= ConfirmPasswordInputWidget_TextProp_OnValueChanged;
        StateMachine.State = null;
    }

    public bool ProcessInputEvent(InputEvent inputEvent)
    {
        if (SignUpFormWidget.IsVisibleProp.IsFalse())
            return false;
        
        return FocusController.ProcessInputEvent(inputEvent);
    }

    private void SignUpFormWidget_IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool wasFocused, bool isFocused)
    {
        if (isFocused)
            FocusController.FocusFirstWidget();
        // else
        //     FocusController.ClearFocus();
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
        
        if (string.IsNullOrWhiteSpace(password))
        {
            PasswordFieldWidget.ErrorTextProperty.Set("Password is required");
            isPasswordValid = false;
        }
        else if (!PasswordValidator.Validate(password))
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
        SubmitButtonWidget.IsInteractableProp.Set(IsEmailValid &&
                                                  IsUsernameValid &&
                                                  IsPasswordValid &&
                                                  IsConfirmPasswordValid &&
                                                  !IsLoading);
    }

    private async void SubmitForm()
    {
        try
        {
            StateMachine.State = new SignUpFormWidgetControllerSubmittingFormState(this);
            await SignUpService.SignUpAsync(Email, Username, Password);
            FormSubmitted?.Invoke();
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