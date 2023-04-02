using System.Linq;

public sealed class MinDigitsPasswordRequirement : IPasswordRequirement
{
    public string Description { get; } 

    private int MinDigits { get; }
    
    public MinDigitsPasswordRequirement(int minDigits)
    {
        MinDigits = minDigits;

        var numbersText = "numbers";
        if (minDigits == 1)
            numbersText = "number";
        
        Description = $"At least {MinDigits} {numbersText}";
    }
    
    public bool IsMet(string password)
    {
        return password.Count(c => char.IsDigit(c)) >= MinDigits;
    }
}