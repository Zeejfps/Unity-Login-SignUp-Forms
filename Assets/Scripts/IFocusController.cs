public delegate void FocusChangedHandler(IFocusable prevFocused, IFocusable currFocused);

public interface IFocusController : IWidgetController
{
    event FocusChangedHandler FocusChanged;

    IFocusable FocusedWidget { get; }
    
    bool CanCycle { get; set; }
    
    void FocusFirstWidget();
    void FocusNext(int skip = 0);
    void FocusPrev(int skip = 0);
    void ClearFocus();

    void Add(IFocusable focusable);
    void Remove(IFocusable focusable);
}