using YADBF;

public interface IPasswordFieldWidget : IWidget
{
    ITextInputWidget TextInputWidget { get; }
    IToggleWidget ShowPasswordToggleWidget { get; }
    ObservableProperty<string> ErrorTextProperty { get; }
}