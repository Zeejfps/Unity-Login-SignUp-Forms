using System;
using Common.Controllers;

namespace SignUpConfirmationForm
{
    public interface ISignUpConfirmationPopupWidgetController : IWidgetPresenter
    {
        event Action Confirmed;
        event Action Canceled;
    }
}