public interface IPasswordValidator
{
    IPasswordValidationResult Validate(string password);
}