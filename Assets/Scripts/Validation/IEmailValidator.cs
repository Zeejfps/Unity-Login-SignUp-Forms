namespace Validation
{
    public interface IEmailValidator
    {
        EmailValidationStatus Validate(string email);
    }
}