using YADBF;

namespace Login
{
    internal sealed class BasicTextInputWidget : ITextInputWidget
    {
        public ObservableProperty<string> TextProp { get; } = new();
        public ObservableProperty<bool> IsInteractableProp { get; } = new(true);
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    }

    internal sealed class BasicPasswordInputWidget : IPasswordInputWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<string> TextProp { get; } = new();
        public ObservableProperty<bool> IsInteractableProp { get; } = new(true);
        public ObservableProperty<IToggleWidget> ShowPasswordToggleWidgetProp { get; } = new();
        public ObservableProperty<bool> IsShowingPasswordProp { get; } = new();

        public BasicPasswordInputWidget()
        {
            ShowPasswordToggleWidgetProp.Set(new ShowPasswordToggleWidget(this));
        }
    }

    internal sealed class ShowPasswordToggleWidget : IToggleWidget
    {
        public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
        public ObservableProperty<bool> IsOnProp { get; }
        public ObservableProperty<bool> IsInteractable { get; }

        private IPasswordInputWidget PasswordInputWidget { get; }
        
        public ShowPasswordToggleWidget(IPasswordInputWidget passwordInputWidget)
        {
            PasswordInputWidget = passwordInputWidget;
            IsOnProp = PasswordInputWidget.IsShowingPasswordProp;
            IsInteractable = PasswordInputWidget.IsInteractableProp;
        }
        
        public void HandleClick()
        {
            PasswordInputWidget.IsShowingPasswordProp.Toggle();
        }
    }
}