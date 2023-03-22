using YADBF;

public interface IPasswordInputWidget : ITextInputWidget
{
    ObservableProperty<IToggleWidget> ShowPasswordToggleWidgetProp { get; }
    ObservableProperty<bool> IsShowingPasswordProp { get; }
}