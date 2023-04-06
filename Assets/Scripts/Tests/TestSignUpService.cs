using System.Threading.Tasks;
using Login;
using SignUpConfirmationForm;

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

            var popupWidget = new SignUpConfirmationPopupWidget();
            var controller = new SignUpConfirmationPopupWidgetController(popupWidget);
            await PopupService.ShowPopupAsync(popupWidget);
            controller.Dispose();
        }
    }
}