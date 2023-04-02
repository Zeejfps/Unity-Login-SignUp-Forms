using System;
using System.Linq;

public abstract class MinCharactersPasswordRequirement : IPasswordRequirement
{
    public string Description { get; protected set; }

    protected int Min { get; }
    private Func<char, bool> Predicate { get; }

    protected MinCharactersPasswordRequirement(int min, Func<char, bool> predicate)
    {
        Min = min;
        Predicate = predicate;
    }
    
    public bool IsMet(string password)
    {
        return password.Count(Predicate.Invoke) >= Min;
    }
}