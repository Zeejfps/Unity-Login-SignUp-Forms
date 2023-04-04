public interface ILoginSignUpPageWidget : IWidget
{
    ILoginFormWidget LoginFormWidget { get; }
    ISignUpFormWidget SignUpFormWidget { get; }
    ITabWidget LoginFormTabWidget { get; }
    ITabWidget SignUpFormTabWidget { get; }
}