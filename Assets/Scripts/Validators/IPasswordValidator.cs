using System.Collections.Generic;

namespace Validators
{
    public interface IPasswordValidator
    {
        IReadOnlyList<IPasswordRequirement> PasswordRequirements { get; }
    }
}