public interface IPasswordFieldWidget : IWidget
{
    ITextInputWidget TextInputWidget { get; }
    IToggleWidget ShowPasswordToggleWidget { get; }
}