using YADBF;

public sealed class TabWidget : ITabWidget
{
    public ObservableProperty<bool> IsSelectedProp { get; } = new();
    public ObservableProperty<bool> IsVisibleProp { get; } = new(true);
    
    private readonly IWidget m_Content;

    public TabWidget(IWidget content)
    {
        m_Content = content;
        IsSelectedProp.Set(m_Content.IsVisibleProp.Value);
        m_Content.IsVisibleProp.Bind(IsSelectedProp);
    }

    public void HandleClick()
    {
        if (!IsSelectedProp.IsTrue())
            IsSelectedProp.Set(true);
    }
}