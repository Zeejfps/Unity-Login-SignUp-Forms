using YADBF;

namespace Common.Widgets
{
    public interface IPasswordRequirementWidget : IWidget
    {
        ObservableProperty<string> Description { get; }
        ObservableProperty<bool> IsMet { get; }
    }
}