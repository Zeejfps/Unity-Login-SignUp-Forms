namespace LoginForm
{
    public interface ILoginFormWidgetPresenter : IWidgetPresenter
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}