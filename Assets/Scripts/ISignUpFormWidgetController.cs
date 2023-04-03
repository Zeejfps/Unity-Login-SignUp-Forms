using System;

public interface ISignUpFormWidgetController
{
    event Action Submitted;
    
    ISignUpFormWidget SignUpFormWidget { get; }

    void Submit();
}