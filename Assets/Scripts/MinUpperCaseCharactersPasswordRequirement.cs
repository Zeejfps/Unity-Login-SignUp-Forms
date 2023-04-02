using System.Linq;

public sealed class MinUpperCaseCharactersPasswordRequirement : IPasswordRequirement
{
    public string Description { get; }
    
    private int Min { get; }

    public MinUpperCaseCharactersPasswordRequirement(int minUpperCaseChars)
    {
        Min = minUpperCaseChars;
        var charactersText = "characters";
        if (minUpperCaseChars == 1)
            charactersText = "character";
        Description = $"At least {minUpperCaseChars} upper case {charactersText}";
    }
    
    public bool IsMet(string password)
    {
        return password.Count(char.IsUpper) >= Min;
    }
}