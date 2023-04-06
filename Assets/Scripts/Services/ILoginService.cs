using System.Threading.Tasks;

namespace Services
{
    public interface ILoginService
    {
        Task LoginAsync(string email, string password);
    }
}