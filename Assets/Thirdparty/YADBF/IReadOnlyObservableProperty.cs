namespace YADBF
{
    public interface IReadOnlyObservableProperty<out T>
    {
        T Value { get; }
    }
}