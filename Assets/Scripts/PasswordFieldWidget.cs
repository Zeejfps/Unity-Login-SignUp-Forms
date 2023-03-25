using YADBF;

namespace Login
{
    internal class PasswordFieldWidget : IPasswordFieldWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ITextInputWidget TextInputWidget { get; }
        public IToggleWidget ShowPasswordToggleWidget { get; }
        public ObservableProperty<string> ErrorTextProperty { get; } = new();

        public PasswordFieldWidget(ITextInputWidget textInputWidget)
        {
            TextInputWidget = textInputWidget;
            TextInputWidget.IsMaskingCharactersProperty.Set(true);
            ShowPasswordToggleWidget = new CharacterMaskToggleWidget(textInputWidget);
        }
    }
}