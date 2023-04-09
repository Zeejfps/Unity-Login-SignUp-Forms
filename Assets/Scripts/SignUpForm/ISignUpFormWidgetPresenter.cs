using System;
using Common.Controllers;

namespace SignUpForm
{
    public interface ISignUpFormWidgetPresenter : IWidgetPresenter
    {
        event Action FormSubmitted;
        event Action EmailChanged;
        event Action UsernameChanged;
        event Action PasswordChanged;
        event Action ConfirmPasswordChanged;
    
        string Email { get; }
        string Username { get; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
    
        bool IsLoading { get; set; }
    
        void ValidateEmail();
        void ValidateUsername();
        void ValidatePassword();
        void ValidateConfirmPassword();
    }
}