public sealed class MinUpperCaseCharactersPasswordRequirement : MinCharactersPasswordRequirement
{
    public MinUpperCaseCharactersPasswordRequirement(int minUpperCaseChars): base(minUpperCaseChars, char.IsUpper)
    {
        var charactersText = "characters";
        if (minUpperCaseChars == 1)
            charactersText = "character";
        Description = $"At least {minUpperCaseChars} upper case {charactersText}";
    }
}