using YADBF;

public sealed class PasswordRequirementWidget : IPasswordRequirementWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> Description { get; } = new();
    public ObservableProperty<bool> IsMet { get; } = new();
}