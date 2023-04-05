using System;

public delegate void FocusChangedHandler(IFocusable prevFocused, IFocusable currFocused);

public interface IFocusController
{
    event FocusChangedHandler FocusChanged;

    IFocusable FocusedWidget { get; }
    
    void FocusFirstWidget();
    void FocusNext();
    void FocusPrev();
    void ClearFocus();

    void Add(IFocusable focusable);

    void Dispose();
}