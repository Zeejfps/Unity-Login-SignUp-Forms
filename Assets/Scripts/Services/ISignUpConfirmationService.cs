using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface ISignUpConfirmationService
    {
        Task ConfirmSignUp(string confirmationCode, CancellationToken cancellationToken = default);
    }
}