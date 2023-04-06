using Common.Controllers;

namespace LoginForm
{
    public interface ILoginFormWidgetController : IWidgetController
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}