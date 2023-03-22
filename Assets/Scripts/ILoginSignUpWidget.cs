using YADBF;

public interface ILoginSignUpWidget : IWidget
{
    ObservableProperty<ITabWidget> LoginFormTabWidgetProp { get; }
    ObservableProperty<ITabWidget> SignUpFormTabWidgetProp { get; }
    ObservableProperty<ILoginFormWidget> LoginFormWidgetProp { get; }
    ObservableProperty<ISignUpFormWidget> SignUpFormWidgetProp { get; }
}