using System.Threading.Tasks;

namespace Login
{
    public interface ILoginService
    {
        Task LoginAsync(string email, string password);
    }
}