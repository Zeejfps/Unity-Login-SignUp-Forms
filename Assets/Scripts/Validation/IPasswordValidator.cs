using System.Collections.Generic;

public interface IPasswordValidator
{
    IReadOnlyList<IPasswordRequirement> PasswordRequirements { get; }
}