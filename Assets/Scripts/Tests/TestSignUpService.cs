using System.Threading;
using System.Threading.Tasks;
using Login;
using Services;
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

        public async Task<bool> SignUpAsync(string email, string username, string password, CancellationToken cancellationToken = default)
        {
            var popupWidget = new SignUpConfirmationPopupWidget();
            var controller = new SignUpConfirmationPopupWidgetController(new TestSignUpConfirmationService(), popupWidget);
            
            var tcs = new TaskCompletionSource<bool>();

            controller.Confirmed += () =>
            {
                tcs.SetResult(true);
            };
            
            controller.Canceled += () =>
            {
                tcs.SetResult(false);
            };

            try
            {
                await Task.Delay(2000, cancellationToken);
                
                await PopupService.ShowPopupAsync(popupWidget);

                return await tcs.Task;
            }
            finally
            {
                controller.Dispose();
            }
        }
    }
}