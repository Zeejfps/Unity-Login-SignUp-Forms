using UnityEngine;
using YADBF.Unity;

namespace Login
{
    public sealed class UGUILoginFormWidgetView : UGUIWidgetView<ILoginFormWidget>
    {
        [SerializeField] private View<ITextInputWidget> m_EmailInputWidgetView;
        [SerializeField] private UGUIPasswordInputWidgetView m_PasswordInputWidgetView;

        protected override void OnBindToModel(ILoginFormWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.IsVisibleProp, gameObject.SetActive);

            m_EmailInputWidgetView.Model = model.EmailInputWidget;
            m_PasswordInputWidgetView.Model = model.PasswordInputWidget;
        }
    }
}