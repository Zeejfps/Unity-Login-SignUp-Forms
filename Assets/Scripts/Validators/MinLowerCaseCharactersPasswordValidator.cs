namespace Validators
{
    public sealed class MinLowerCaseCharactersPasswordValidator : MinCharactersPasswordValidator
    {
        public MinLowerCaseCharactersPasswordValidator(int minLowerChaseCharacters) : base(minLowerChaseCharacters, char.IsLower)
        {
            var charactersText = "characters";
            if (minLowerChaseCharacters == 1)
                charactersText = "character";
            Description = $"At least {minLowerChaseCharacters} lower case {charactersText}";
        }
    }
}