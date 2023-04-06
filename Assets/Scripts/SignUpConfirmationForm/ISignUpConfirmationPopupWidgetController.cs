using System;

namespace SignUpConfirmationForm
{
    public interface ISignUpConfirmationPopupWidgetController : IWidgetController
    {
        event Action Confirmed;
        event Action Canceled;
    }
}