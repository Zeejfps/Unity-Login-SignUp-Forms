using System.Threading.Tasks;
using Services;

namespace Tests
{
    public sealed class TestLoginService : ILoginService
    {
        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            await Task.Delay(2000);
            return LoginResult.ErrorInvalidCredentials;
        }
    }
}