using Login;
using SignUpConfirmationForm;
using UnityEngine;

namespace UGUI
{
    public sealed class UGUIConfirmationPopupWidgetView : UGUIWidgetView<ISignUpConfirmationPopupWidget>
    {
        [SerializeField] private UGUITextInputWidgetView m_TextInputWidgetView;
        [SerializeField] private UGUIButtonWidgetView m_CancelButtonWidgetView;
        [SerializeField] private UGUIButtonWidgetView m_ConfirmButtonWidgetView;
    
        protected override void OnBindToModel(ISignUpConfirmationPopupWidget model)
        {
            base.OnBindToModel(model);
            m_TextInputWidgetView.Model = model.CodeInputWidget;
            m_ConfirmButtonWidgetView.Model = model.ConfirmButtonWidget;
            m_CancelButtonWidgetView.Model = model.CancelButtonWidget;
        }
    }
}