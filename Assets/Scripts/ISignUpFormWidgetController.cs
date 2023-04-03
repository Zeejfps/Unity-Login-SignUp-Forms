using System;
using Tests;

public interface ISignUpFormWidgetController
{
    event Action Submitted;
    
    ISignUpFormWidget SignUpFormWidget { get; }
    
    IStateMachine StateMachine { get; }
}