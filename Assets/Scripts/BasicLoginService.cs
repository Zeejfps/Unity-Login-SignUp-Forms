using System.Threading.Tasks;

namespace Login
{
    internal sealed class BasicLoginService : ILoginService
    {
        private IPopupService PopupService { get; }

        public BasicLoginService(IPopupService popupService)
        {
            PopupService = popupService;
        }

        public async Task LoginAsync(string email, string password)
        {
            await Task.Delay(2000);
            await PopupService.ShowInfoPopupAsync("Invalid Credentials", "Email and/or Password was incorrect");
        }
    }
}