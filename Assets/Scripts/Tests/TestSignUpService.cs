using System.Threading.Tasks;
using Login;

namespace Tests
{
    public sealed class TestSignUpService : ISignUpService
    {
        private IPopupManager PopupService { get; }

        public TestSignUpService(IPopupManager popupService)
        {
            PopupService = popupService;
        }

        public async Task SignUpAsync(string email, string username, string password)
        {
            await Task.Delay(2000);

            var test = new TestSignUpConfirmationForm();
            await PopupService.ShowPopupAsync(new ConfirmSignUpPopupWidget(test));
        }
    }
}