public sealed class MinLengthPasswordRequirement : IPasswordRequirement
{
    public string Description { get; }

    private int MinLength { get; }
        
    public MinLengthPasswordRequirement(int minLength)
    {
        MinLength = minLength;
        Description = $"Minimum {MinLength} characters";
    }
        
    public bool IsMet(string password)
    {
        return password.Length >= MinLength;
    }
}