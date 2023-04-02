public interface IPasswordValidationResult
{
    IPasswordRequirement FailedRequirement { get; }
}