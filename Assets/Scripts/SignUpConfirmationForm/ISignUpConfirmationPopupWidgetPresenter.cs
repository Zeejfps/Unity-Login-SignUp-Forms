using System;

namespace SignUpConfirmationForm
{
    public interface ISignUpConfirmationPopupWidgetPresenter : IWidgetPresenter
    {
        event Action Confirmed;
        event Action Canceled;
    }
}