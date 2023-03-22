using System.Threading.Tasks;

namespace Login
{
    internal sealed class BasicLoginService : ILoginService
    {
        public async Task<LoginError> LoginAsync(string email, string password)
        {
            await Task.Delay(2000);
            return LoginError.InvalidCredentials;
        }
    }
}