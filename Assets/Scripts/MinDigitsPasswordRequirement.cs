public sealed class MinDigitsPasswordRequirement : MinCharactersPasswordRequirement
{
    public MinDigitsPasswordRequirement(int minDigits) : base(minDigits, char.IsDigit)
    {
        var charactersText = "characters";
        if (minDigits == 1)
            charactersText = "character";
        
        Description = $"At least {Min} number {charactersText}";
    }
}