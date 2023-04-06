namespace Validators
{
    public sealed class MinLengthPasswordRequirement : IPasswordRequirement
    {
        public string Description { get; }

        private int MinLength { get; }
        
        public MinLengthPasswordRequirement(int minLength)
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