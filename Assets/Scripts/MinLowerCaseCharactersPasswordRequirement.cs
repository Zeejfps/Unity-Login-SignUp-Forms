using System.Linq;

public sealed class MinLowerCaseCharactersPasswordRequirement : IPasswordRequirement
{
    public string Description { get; }
    
    private int Min { get; }

    public MinLowerCaseCharactersPasswordRequirement(int minLowerChaseCharacters)
    {
        Min = minLowerChaseCharacters;
        var charactersText = "characters";
        if (minLowerChaseCharacters == 1)
            charactersText = "character";
        Description = $"At least {minLowerChaseCharacters} lower case {charactersText}";
    }
    
    public bool IsMet(string password)
    {
        return password.Count(char.IsLower) >= Min;
    }
}