public delegate void FocusChangedHandler(IInteractable prevFocused, IInteractable currFocused);

public interface IWidgetFocusController : IWidgetController
{
    event FocusChangedHandler FocusChanged;

    IInteractable FocusedWidget { get; }
    
    bool CanCycle { get; set; }
    
    void FocusFirstWidget();
    void FocusNext(int skip = 0);
    void FocusPrev(int skip = 0);
    void ClearFocus();

    void Add(IInteractable focusable);
    void Remove(IInteractable focusable);
}