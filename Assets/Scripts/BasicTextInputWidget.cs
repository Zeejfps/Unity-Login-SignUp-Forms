using YADBF;

namespace Login
{
    internal sealed class BasicTextInputWidget : ITextInputWidget
    {
        public ObservableProperty<string> TextProp { get; } = new();
        public ObservableProperty<bool> IsInteractableProp { get; } = new(true);
        public ObservableProperty<bool> IsMaskingCharacters { get; } = new();
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    }

    internal sealed class BasicPasswordInputWidget : IPasswordInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<ITextInputWidget> TextInputWidgetProp { get; } = new();
        public ObservableProperty<IToggleWidget> ShowPasswordToggleWidgetProp { get; } = new();

        public BasicPasswordInputWidget()
        {
            TextInputWidgetProp.Set(new BasicTextInputWidget());
            TextInputWidgetProp.Value.IsMaskingCharacters.Set(true);
            ShowPasswordToggleWidgetProp.Set(new ShowPasswordToggleWidget(TextInputWidgetProp.Value));
        }
    }

    internal sealed class ShowPasswordToggleWidget : IToggleWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsOnProp { get; } = new();
        public ObservableProperty<bool> IsInteractable { get; }

        private ITextInputWidget PasswordInputWidget { get; }
        
        public ShowPasswordToggleWidget(ITextInputWidget textInputWidget)
        {
            PasswordInputWidget = textInputWidget;
            PasswordInputWidget.IsMaskingCharacters.ValueChanged += IsMaskingCharacters_OnValueChanged;
            IsInteractable = PasswordInputWidget.IsInteractableProp;
            
            IsOnProp.Value = !PasswordInputWidget.IsMaskingCharacters.Value;
        }

        private void IsMaskingCharacters_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool currvalue)
        {
            IsOnProp.Set(!currvalue);
        }

        public void HandleClick()
        {
            PasswordInputWidget.IsMaskingCharacters.Toggle();
        }
    }
}