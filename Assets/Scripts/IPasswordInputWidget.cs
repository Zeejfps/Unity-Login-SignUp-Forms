using YADBF;

public interface IPasswordInputWidget : IWidget
{
    ITextInputWidget TextInputWidget { get; }
    IToggleWidget ShowPasswordToggleWidget { get; }
}