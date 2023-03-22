using System.Threading.Tasks;

namespace Login
{
    public interface ISignUpService
    {
        Task SignUp(string email, string password);
    }
}