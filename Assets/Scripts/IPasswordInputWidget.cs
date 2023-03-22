using YADBF;

public interface IPasswordInputWidget : IWidget
{
    ObservableProperty<ITextInputWidget> TextInputWidgetProp { get; }
    ObservableProperty<IToggleWidget> ShowPasswordToggleWidgetProp { get; }
}