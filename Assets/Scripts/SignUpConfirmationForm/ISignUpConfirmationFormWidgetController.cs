using System;
using YADBF;

namespace SignUpConfirmationForm
{
    public interface ISignUpConfirmationFormWidgetController
    {
        event Action<ISignUpConfirmationFormWidgetController> FormSubmitted;
        
        ObservableProperty<bool> IsLoadingProp { get; }
        ObservableProperty<Action> ConfirmActionProp { get; }
        ObservableProperty<string> ConfirmationCodeTextProp { get; }
        
        void Cancel();
    }
}