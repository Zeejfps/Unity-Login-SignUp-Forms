namespace Validators
{
    public sealed class MinLengthPasswordValidator : IPasswordValidator
    {
        public string Description { get; }

        private int MinLength { get; }
        
        public MinLengthPasswordValidator(int minLength)
        {
            MinLength = minLength;
            Description = $"Minimum length of {MinLength} characters";
        }
        
        public bool Validate(string password)
        {
            return password.Length >= MinLength;
        }
    }
}