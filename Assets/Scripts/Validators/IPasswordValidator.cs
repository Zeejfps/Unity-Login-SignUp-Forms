namespace Validators
{
    public interface IPasswordValidator
    {
        string Description { get; }
        bool Validate(string password);
    }
}