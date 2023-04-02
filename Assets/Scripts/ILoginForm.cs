﻿using System;
using YADBF;

public interface ILoginForm
{
    ObservableProperty<string> EmailProp { get; }
    ObservableProperty<string> PasswordProp { get; }
    ObservableProperty<bool> IsLoadingProp { get; }
    ObservableProperty<Action> SubmitActionProp { get; }
    EmailValidationStatus IsEmailValid { get; }
    
    bool IsRememberMeChecked { get; set; }
}