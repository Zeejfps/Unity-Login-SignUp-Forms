using System.Threading.Tasks;

namespace Login
{
    public sealed class BasicSignUpService : ISignUpService
    {
        public Task SignUp(string email, string password)
        {
            return Task.Delay(2000);
        }
    }
}