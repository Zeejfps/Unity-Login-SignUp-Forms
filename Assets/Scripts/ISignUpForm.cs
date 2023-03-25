using System;
using YADBF;

public enum EmailValidationResult
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
    ObservableProperty<string> UsernameProp { get; }
    ObservableProperty<string> PasswordProp { get; }
    ObservableProperty<string> ConfirmPasswordProp { get; }
    ObservableProperty<Action> SubmitActionProp { get; }

    EmailValidationResult ValidateEmail();
}