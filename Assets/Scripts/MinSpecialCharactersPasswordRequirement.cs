public sealed class MinSpecialCharactersPasswordRequirement : MinCharactersPasswordRequirement
{
    public MinSpecialCharactersPasswordRequirement(int min) : base(min, c => !char.IsLetterOrDigit(c))
    {
        Description = $"At least {Min} special characters";
    }
}