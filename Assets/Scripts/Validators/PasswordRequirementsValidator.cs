using System.Collections.Generic;

namespace Validators
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