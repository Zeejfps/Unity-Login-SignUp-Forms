public delegate void FocusChangedHandler(IFocusable prevFocused, IFocusable currFocused);

public interface IFocusController : IWidgetController
{
    event FocusChangedHandler FocusChanged;

    IFocusable FocusedWidget { get; }
    
    bool CanCycle { get; set; }
    
    void FocusFirstWidget();
    void FocusNext();
    void FocusPrev();
    void ClearFocus();

    void Add(IFocusable focusable);
}