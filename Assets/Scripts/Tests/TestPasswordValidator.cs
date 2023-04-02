using System.Collections.Generic;

namespace Login
{
    public sealed class TestPasswordValidator : IPasswordValidator
    {
        private List<IPasswordRequirement> PasswordRequirements { get; }

        public TestPasswordValidator(List<IPasswordRequirement> passwordRequirements)
        {
            PasswordRequirements = passwordRequirements;
        }
        
        public IPasswordValidationResult Validate(string password)
        {
            foreach (var requirement in PasswordRequirements)
            {
                if (!requirement.IsMet(password))
                    return new ValidationFailed(requirement);
            }

            return new ValidationSuccess();
        }
    }
}