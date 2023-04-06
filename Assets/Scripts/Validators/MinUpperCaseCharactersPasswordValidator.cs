namespace Validators
{
    public sealed class MinUpperCaseCharactersPasswordValidator : MinCharactersPasswordValidator
    {
        public MinUpperCaseCharactersPasswordValidator(int minUpperCaseChars): base(minUpperCaseChars, char.IsUpper)
        {
            var charactersText = "characters";
            if (minUpperCaseChars == 1)
                charactersText = "character";
            Description = $"At least {minUpperCaseChars} upper case {charactersText}";
        }
    }
}