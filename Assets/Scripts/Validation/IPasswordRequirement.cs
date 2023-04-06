public interface IPasswordRequirement
{
    string Description { get; }
    bool Validate(string password);
}