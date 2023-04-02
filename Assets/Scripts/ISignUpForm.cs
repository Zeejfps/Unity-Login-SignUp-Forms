using System;
using YADBF;

public enum EmailValidationStatus
{
    Valid,
    Empty,
    Invalid
}

public interface ISignUpForm
{
    event Action Submitted;
        
    ObservableProperty<bool> IsLoadingProp { get; }
    ObservableProperty<string> EmailProp { get; }
    EmailValidationStatus EmailValidationResult { get; }
    ObservableProperty<string> UsernameProp { get; }
    ObservableProperty<string> PasswordProp { get; }
    ObservableProperty<string> ConfirmPasswordProp { get; }
    ObservableProperty<Action> SubmitActionProp { get; }
    IPasswordRequirement[] PasswordRequirements { get; }
}