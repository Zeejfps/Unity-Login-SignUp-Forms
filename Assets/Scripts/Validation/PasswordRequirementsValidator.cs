namespace Validation
{
    public sealed class PasswordRequirementsValidator : IPasswordValidator
    {
        public IPasswordRequirement[] PasswordRequirements { get; }

        public PasswordRequirementsValidator(IPasswordRequirement[] passwordRequirements)
        {
            PasswordRequirements = passwordRequirements;
        }
        
        public bool Validate(string password)
        {
            foreach (var requirement in PasswordRequirements)
            {
                if (!requirement.Validate(password))
                    return false;
            }

            return true;
        }
    }
}