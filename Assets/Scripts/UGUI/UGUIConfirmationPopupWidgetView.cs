using Login;
using UnityEngine;

namespace UGUI
{
    public sealed class UGUIConfirmationPopupWidgetView : UGUIWidgetView<IConfirmSignUpPopupWidget>
    {
        [SerializeField] private UGUITextInputWidgetView m_TextInputWidgetView;
        [SerializeField] private UGUIButtonWidgetView m_CancelButtonWidgetView;
        [SerializeField] private UGUIButtonWidgetView m_ConfirmButtonWidgetView;
    
        protected override void OnBindToModel(IConfirmSignUpPopupWidget model)
        {
            base.OnBindToModel(model);
            m_TextInputWidgetView.Model = model.CodeInputWidget;
            m_ConfirmButtonWidgetView.Model = model.ConfirmButtonWidget;
            m_CancelButtonWidgetView.Model = model.CancelButtonWidget;
        }
    }
}