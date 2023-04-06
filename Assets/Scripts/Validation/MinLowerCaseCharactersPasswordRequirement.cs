namespace Validation
{
    public sealed class MinLowerCaseCharactersPasswordRequirement : MinCharactersPasswordRequirement
    {
        public MinLowerCaseCharactersPasswordRequirement(int minLowerChaseCharacters) : base(minLowerChaseCharacters, char.IsLower)
        {
            var charactersText = "characters";
            if (minLowerChaseCharacters == 1)
                charactersText = "character";
            Description = $"At least {minLowerChaseCharacters} lower case {charactersText}";
        }
    }
}