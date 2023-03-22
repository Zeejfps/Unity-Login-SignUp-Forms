using System;
using System.Threading.Tasks;
using YADBF;

namespace Login
{
    internal sealed class TestLoginManager : ILoginManager
    {
        public ObservableProperty<string> EmailProp { get; } = new();
        public ObservableProperty<string> PasswordProp { get; } = new();
        public ObservableProperty<bool> IsLoadingProp { get; } = new();
        public ObservableProperty<Action> LoginActionProp { get; } = new();

        private IPopupService PopupService { get; }

        public TestLoginManager(IPopupService popupService)
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