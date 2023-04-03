using System;

public interface ISignUpFormWidgetController
{
    event Action Submitted;
    
    ISignUpFormWidget SignUpFormWidget { get; }
    
    string ConfirmPassword { get; set; }
    bool IsLoading { get; set; }

    void ValidateEmail();
    void ValidatePassword();
    void ValidateConfirmPassword();
}