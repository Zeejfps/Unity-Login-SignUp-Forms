using System.Threading;
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

        public async Task SignUpAsync(string email, string username, string password, CancellationToken cancellationToken = default)
        {
            await Task.Delay(2000, cancellationToken);
            
            var popupWidget = new SignUpConfirmationPopupWidget();
            var controller = new SignUpConfirmationPopupWidgetController(new TestSignUpConfirmationService(), popupWidget);
            
            await PopupService.ShowPopupAsync(popupWidget);

            controller.Dispose();
        }
    }
}