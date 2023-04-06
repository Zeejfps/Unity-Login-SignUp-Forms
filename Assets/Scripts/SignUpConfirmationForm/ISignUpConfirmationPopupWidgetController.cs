using System;
using YADBF;

namespace SignUpConfirmationForm
{
    public interface ISignUpConfirmationPopupWidgetController : IWidgetController
    {
        event Action<ISignUpConfirmationPopupWidgetController> FormSubmitted;
    }
}