using System.Threading.Tasks;

namespace Login
{
    public enum LoginError
    {
        None,
        InvalidCredentials,
    }
    
    public interface ILoginService
    {
        Task<LoginError> LoginAsync(string email, string password);
    }
}