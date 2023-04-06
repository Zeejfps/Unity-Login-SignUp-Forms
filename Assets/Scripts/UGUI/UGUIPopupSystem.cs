using Login;
using UnityEngine;
using YADBF;
using YADBF.Unity;

namespace UGUI
{
    public sealed class UGUIPopupSystem : MonoBehaviour
    {
        [SerializeField] private GameObject m_ScreenDimmer;
        [SerializeField] private UGUIConfirmationPopupWidgetView m_ConfirmationPopupWidgetViewPrefab;
        [SerializeField] private UGUIInfoPopupWidgetView m_InfoPopupWidgetViewPrefab;
    
        private IPopupManager m_PopupManager;
        private IPopupManager PopupManager => m_PopupManager ??= Z.Get<IPopupManager>();

        private View m_PopupView;
    
        private void Start()
        {
            PopupManager.PopupWidgetProp.ValueChanged += PopupWidgetProp_OnValueChanged;
        }

        private void OnDestroy()
        {
            PopupManager.PopupWidgetProp.ValueChanged -= PopupWidgetProp_OnValueChanged;
        }

        private void PopupWidgetProp_OnValueChanged(ObservableProperty<IPopupWidget> property, IPopupWidget prevvalue, IPopupWidget currvalue)
        {
            HidePopup();
            if (currvalue != null)
                ShowPopup(currvalue);
        }

        private void ShowPopup(IPopupWidget widget)
        {
            m_ScreenDimmer.SetActive(true);

            View prefab = null;
            switch (widget)
            {
                case ISignUpConfirmationPopupWidget:
                    prefab = m_ConfirmationPopupWidgetViewPrefab;
                    break;
                case IInfoPopupWidget:
                    prefab = m_InfoPopupWidgetViewPrefab;
                    break;
            }
        
            m_PopupView = Instantiate(prefab, transform);
            m_PopupView.TrySetViewModel(widget);

            widget.IsVisibleProp.ValueChanged += PopupWidget_IsVisibleProp_OnValueChanged;
        }

        private void HidePopup()
        {
            if (m_PopupView == null)
                return;

            m_ScreenDimmer.SetActive(false);

            var popup = (IPopupWidget)m_PopupView.GetModel();
            popup.IsVisibleProp.ValueChanged -= PopupWidget_IsVisibleProp_OnValueChanged;
        
            var go = m_PopupView.gameObject;
            go.SetActive(false);
            Destroy(go);
            m_PopupView = null;
        }

        private void PopupWidget_IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isVisible)
        {
            if (!isVisible)
                HidePopup();
        }
    }
}
