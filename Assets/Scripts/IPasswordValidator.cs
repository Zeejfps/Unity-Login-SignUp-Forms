public interface IPasswordValidator
{
    IPasswordRequirement[] PasswordRequirements { get; }
    bool Validate(string password);
}