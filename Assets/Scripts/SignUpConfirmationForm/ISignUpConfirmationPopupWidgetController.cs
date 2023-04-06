using System;
using Common.Controllers;

namespace SignUpConfirmationForm
{
    public interface ISignUpConfirmationPopupWidgetController : IWidgetController
    {
        event Action Confirmed;
        event Action Canceled;
    }
}