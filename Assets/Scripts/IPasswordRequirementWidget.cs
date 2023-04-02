using YADBF;

public interface IPasswordRequirementWidget : IWidget
{
    ObservableProperty<string> Description { get; }
    ObservableProperty<bool> IsMet { get; }
}