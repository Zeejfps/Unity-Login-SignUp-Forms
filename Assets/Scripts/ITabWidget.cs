using YADBF;

public interface ITabWidget : IWidget
{
    ObservableProperty<bool> IsSelectedProp { get; }

    void HandleClick();
}