namespace Validators
{
    public sealed class MinSpecialCharactersPasswordValidator : MinCharactersPasswordValidator
    {
        public MinSpecialCharactersPasswordValidator(int min) : base(min, c => !char.IsLetterOrDigit(c))
        {
            Description = $"At least {Min} special characters";
        }
    }
}