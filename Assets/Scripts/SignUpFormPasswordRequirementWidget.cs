using YADBF;

public sealed class SignUpFormPasswordRequirementWidget : IPasswordRequirementWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> Description { get; } = new();
    public ObservableProperty<bool> IsMet { get; } = new();

    private IPasswordRequirement PasswordRequirement { get; }
    
    public SignUpFormPasswordRequirementWidget(IPasswordRequirement passwordRequirement)
    {
        PasswordRequirement = passwordRequirement;
        
        Description.Set(passwordRequirement.Description);
    }
}