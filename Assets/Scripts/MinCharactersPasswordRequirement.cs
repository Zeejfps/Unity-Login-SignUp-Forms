using System;
using System.Linq;
using YADBF;

public abstract class MinCharactersPasswordRequirement : IPasswordRequirement
{
    public string Description { get; protected set; }
    public ObservableProperty<bool> IsMetProperty { get; }

    protected int Min { get; }
    private Func<char, bool> Predicate { get; }

    protected MinCharactersPasswordRequirement(int min, Func<char, bool> predicate)
    {
        Min = min;
        Predicate = predicate;
    }
    
    public bool Validate(string password)
    {
        return password.Count(Predicate.Invoke) >= Min;
    }
}