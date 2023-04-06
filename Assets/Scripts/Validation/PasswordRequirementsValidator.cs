using System.Collections.Generic;

namespace Validation
{
    public sealed class PasswordRequirementsValidator : IPasswordValidator
    {
        public IReadOnlyList<IPasswordRequirement> PasswordRequirements { get; }

        public PasswordRequirementsValidator(IPasswordRequirement[] passwordRequirements)
        {
            PasswordRequirements = passwordRequirements;
        }
    }
}