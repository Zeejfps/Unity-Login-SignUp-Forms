using InfoPopup;
using Login;
using SignUpConfirmationForm;
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
            DestroyPopup();
            if (currvalue != null)
                CreateAndShowPopup(currvalue);
        }

        private void CreateAndShowPopup(IPopupWidget widget)
        {
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

            widget.IsVisibleProperty.ValueChanged += PopupWidget_IsVisibleProp_OnValueChanged;
            
            ShowPopup();
        }

        private void DestroyPopup()
        {
            if (m_PopupView == null)
                return;
            
            var popup = (IPopupWidget)m_PopupView.GetModel();
            popup.IsVisibleProperty.ValueChanged -= PopupWidget_IsVisibleProp_OnValueChanged;
            
            HidePopup();
            
            var go = m_PopupView.gameObject;
            Destroy(go);
            m_PopupView = null;
        }

        private void ShowPopup()
        {
            m_ScreenDimmer.SetActive(true);
            m_PopupView.gameObject.SetActive(true);
        }

        private void HidePopup()
        {
            m_ScreenDimmer.SetActive(false);
            m_PopupView.gameObject.SetActive(false);
        }
        
        private void PopupWidget_IsVisibleProp_OnValueChanged(ObservableProperty<bool> property, bool prevvalue, bool isVisible)
        {
            if (isVisible)
            {
                ShowPopup();
            }
            else
            {
                HidePopup();
            }
        }
    }
}
