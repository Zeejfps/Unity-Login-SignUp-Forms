using YADBF.Unity;

namespace Login
{
    public abstract class WidgetView<TWidget> : View<TWidget> where TWidget : class, IWidget
    {
        protected override void OnBindToModel(TWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.IsVisibleProp, gameObject.SetActive);
        }
    }
}