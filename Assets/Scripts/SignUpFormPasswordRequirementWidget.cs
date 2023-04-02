using UnityEngine;
using YADBF;

public sealed class SignUpFormPasswordRequirementWidget : IPasswordRequirementWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ObservableProperty<string> Description { get; } = new();
    public ObservableProperty<bool> IsMet { get; } = new();

    private ITextInputWidget PasswordInputWidget { get; }
    private IPasswordRequirement PasswordRequirement { get; }
    
    public SignUpFormPasswordRequirementWidget(ITextInputWidget passwordInputWidget, IPasswordRequirement passwordRequirement)
    {
        PasswordInputWidget = passwordInputWidget;
        PasswordRequirement = passwordRequirement;
        Description.Set(passwordRequirement.Description);
        
        passwordInputWidget.TextProp.ValueChanged += TextProp_OnValueChanged;
    }

    private void TextProp_OnValueChanged(ObservableProperty<string> property, string prevvalue, string currvalue)
    {
        IsMet.Set(PasswordRequirement.IsMet(currvalue));
    }
}