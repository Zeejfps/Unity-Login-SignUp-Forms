public interface IPasswordRequirement
{
    string Description { get; }
    bool IsMet(string password);
}