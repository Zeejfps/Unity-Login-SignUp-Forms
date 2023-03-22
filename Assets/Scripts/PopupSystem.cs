using System;
using Login;
using UnityEngine;
using YADBF;

public sealed class PopupSystem : MonoBehaviour
{
    [SerializeField] private GameObject m_ScreenDimmer;
    [SerializeField] private InfoPopupWidgetView m_InfoPopupWidgetViewPrefab;
    
    private IPopupService m_PopupService;
    private IPopupService PopupService => m_PopupService ??= Z.Get<IPopupService>();

    private InfoPopupWidgetView m_InfoPopupWidgetView;
    
    private void Start()
    {
        PopupService.InfoPopupWidgetProp.ValueChanged += InfoPopupWidgetProp_OnValueChanged;
    }

    private void OnDestroy()
    {
        PopupService.InfoPopupWidgetProp.ValueChanged -= InfoPopupWidgetProp_OnValueChanged;
    }

    private void InfoPopupWidgetProp_OnValueChanged(ObservableProperty<IInfoPopupWidget> property, IInfoPopupWidget prevvalue, IInfoPopupWidget currvalue)
    {
        if (currvalue == null && m_InfoPopupWidgetView != null)
        {
            HidePopup();
            return;
        }
        
        if (currvalue != null && m_InfoPopupWidgetView != null)
        {
            m_InfoPopupWidgetView.Model = currvalue;
            return;
        }

        if (currvalue != null && m_InfoPopupWidgetView == null)
        {
            ShowPopup(currvalue);
            return;
        }
    }

    private void ShowPopup(IInfoPopupWidget widget)
    {
        m_ScreenDimmer.SetActive(true);
        m_InfoPopupWidgetView = Instantiate(m_InfoPopupWidgetViewPrefab, transform);
        m_InfoPopupWidgetView.Model = widget;
    }

    private void HidePopup()
    {
        m_ScreenDimmer.SetActive(false);
        var infoPopupWidgetView = m_InfoPopupWidgetView;
        var go = infoPopupWidgetView.gameObject;
        go.SetActive(false);
        Destroy(go);
        m_InfoPopupWidgetView = null;
    }
}
