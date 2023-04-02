namespace Login
{
    public sealed class ValidationFailed : IPasswordValidationResult
    {
        public IPasswordRequirement FailedRequirement { get; }

        public ValidationFailed(IPasswordRequirement failedRequirement)
        {
            FailedRequirement = failedRequirement;
        }
    }
}