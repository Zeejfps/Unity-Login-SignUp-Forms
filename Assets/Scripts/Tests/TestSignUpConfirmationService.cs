using System.Threading;
using System.Threading.Tasks;
using Services;
using SignUpConfirmationForm;

namespace Tests
{
    public sealed class TestSignUpConfirmationService : ISignUpConfirmationService
    {
        public Task ConfirmSignUp(string confirmationCode, CancellationToken cancellationToken = default)
        {
            return Task.Delay(3000, cancellationToken);
        }
    }
}