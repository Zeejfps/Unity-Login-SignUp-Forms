using System.Threading;
using System.Threading.Tasks;

namespace Login
{
    public interface IConfirmSignUpService
    {
        Task ConfirmSignUp(string confirmationCode, CancellationToken cancellationToken = default);
    }
}