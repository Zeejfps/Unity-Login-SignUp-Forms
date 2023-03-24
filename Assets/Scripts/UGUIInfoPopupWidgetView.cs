using Login;
using UnityEngine;

public sealed class UGUIInfoPopupWidgetView : UGUIWidgetView<IInfoPopupWidget>
{
    [SerializeField] private UGUIButtonWidgetView m_ButtonWidgetView;
    
    protected override void OnBindToModel(IInfoPopupWidget model)
    {
        base.OnBindToModel(model);
        m_ButtonWidgetView.Model = new ActionPropertyButtonWidget(model.OkActionProp);
    }
}