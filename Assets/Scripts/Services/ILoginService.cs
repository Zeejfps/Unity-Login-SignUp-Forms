using System.Threading.Tasks;

namespace Services
{
    public enum LoginResult
    {
        Success,
        ErrorInvalidCredentials
    }
    
    public interface ILoginService
    {
        Task<LoginResult> LoginAsync(string email, string password);
    }
}