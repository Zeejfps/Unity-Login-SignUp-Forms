using Login;
using Widgets;
using YADBF;

public sealed class PasswordFieldWidget : IPasswordFieldWidget
{
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    public ITextInputWidget TextInputWidget { get; } = new TextInputWidget();
    public IToggleWidget ShowPasswordToggleWidget { get; }
    public ObservableProperty<string> ErrorTextProperty { get; } = new();

    public PasswordFieldWidget(ITextInputWidget textInputWidget) : this()
    {
        TextInputWidget = textInputWidget;
    }
    
    public PasswordFieldWidget()
    {
        ShowPasswordToggleWidget = new CharacterMaskToggleWidget(TextInputWidget);
        TextInputWidget.IsMaskingCharactersProperty.Set(true);
    }
}