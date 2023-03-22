using YADBF;

public interface ILoginSignUpWidget : IWidget
{
    ITabWidget LoginFormTabWidget { get; }
    ITabWidget SignUpFormTabWidget { get; }
    ILoginFormWidget LoginFormWidget { get; }
    ISignUpFormWidget SignUpFormWidget { get; }
}