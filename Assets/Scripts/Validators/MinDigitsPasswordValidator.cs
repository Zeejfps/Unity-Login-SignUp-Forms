namespace Validators
{
    public sealed class MinDigitsPasswordValidator : MinCharactersPasswordValidator
    {
        public MinDigitsPasswordValidator(int minDigits) : base(minDigits, char.IsDigit)
        {
            var charactersText = "characters";
            if (minDigits == 1)
                charactersText = "character";
        
            Description = $"At least {Min} number {charactersText}";
        }
    }
}