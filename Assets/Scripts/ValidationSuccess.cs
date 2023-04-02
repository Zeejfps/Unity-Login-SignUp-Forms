namespace Login
{
    public sealed class ValidationSuccess : IPasswordValidationResult
    {
        public IPasswordRequirement FailedRequirement { get; }
    }
}