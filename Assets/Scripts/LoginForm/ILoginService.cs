using System.Threading.Tasks;

public interface ILoginService
{
    Task LoginAsync(string email, string password);
}