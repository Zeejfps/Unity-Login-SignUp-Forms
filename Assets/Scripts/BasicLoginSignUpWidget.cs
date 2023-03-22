using YADBF;

public sealed class BasicLoginSignUpWidget : ILoginSignUpWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new();
    public ObservableProperty<ITabWidget> LoginFormTabWidgetProp { get; } = new();
    public ObservableProperty<ITabWidget> SignUpFormTabWidgetProp { get; } = new();
    public ObservableProperty<ILoginFormWidget> LoginFormWidgetProp { get; } = new();
    public ObservableProperty<ISignUpFormWidget> SignUpFormWidgetProp { get; } = new();

    public BasicLoginSignUpWidget(ILoginFormWidget loginFormWidget, ISignUpFormWidget signUpFormWidget)
    {
        LoginFormWidgetProp.Set(loginFormWidget);
        LoginFormTabWidgetProp.Set(new TabWidget(loginFormWidget));
        
        SignUpFormWidgetProp.Set(signUpFormWidget);
        SignUpFormTabWidgetProp.Set(new TabWidget(signUpFormWidget));

        var tabGroup = new TabGroup();
        tabGroup.AddTab(LoginFormTabWidgetProp.Value);
        tabGroup.AddTab(SignUpFormTabWidgetProp.Value);
        LoginFormTabWidgetProp.Value.IsSelectedProp.Set(true);
    }
}