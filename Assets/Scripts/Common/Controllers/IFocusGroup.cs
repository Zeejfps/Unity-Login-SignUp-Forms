using System;

namespace Common.Controllers
{
    public delegate void FocusChangedHandler(IInteractableWidget prevFocused, IInteractableWidget currFocused);

    public interface IFocusGroup : IDisposable
    {
        event FocusChangedHandler FocusChanged;

        IInteractableWidget FocusedWidget { get; }
    
        bool CanCycle { get; set; }
    
        void FocusFirstWidget();
        void FocusNext(int skip = 0);
        void FocusPrev(int skip = 0);
        void ClearFocus();

        void Add(IInteractableWidget focusable);
        void Remove(IInteractableWidget focusable);
    }
}