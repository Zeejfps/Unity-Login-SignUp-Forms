using System.Threading;
using System.Threading.Tasks;

namespace SignUpConfirmationForm
{
    public interface ISignUpConfirmationService
    {
        Task ConfirmSignUp(string confirmationCode, CancellationToken cancellationToken = default);
    }
}