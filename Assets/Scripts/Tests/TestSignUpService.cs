using System.Threading.Tasks;

namespace Tests
{
    public sealed class TestSignUpService : ISignUpService
    {
        public Task SignUpAsync(string email, string username, string password)
        {
            return Task.Delay(2000);
        }
    }
}