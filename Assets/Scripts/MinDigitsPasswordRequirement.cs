using System.Linq;

public sealed class MinDigitsPasswordRequirement : IPasswordRequirement
{
    public string Description { get; } 

    private int MinDigits { get; }
    
    public MinDigitsPasswordRequirement(int minDigits)
    {
        MinDigits = minDigits;

        var charactersText = "characters";
        if (minDigits == 1)
            charactersText = "character";
        
        Description = $"At least {MinDigits} number {charactersText}";
    }
    
    public bool IsMet(string password)
    {
        return password.Count(char.IsDigit) >= MinDigits;
    }
}