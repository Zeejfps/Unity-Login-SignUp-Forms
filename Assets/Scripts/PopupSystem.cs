using System;
using Login;
using UnityEngine;
using YADBF;

public sealed class PopupSystem : MonoBehaviour
{
    [SerializeField] private GameObject m_ScreenDimmer;
    [SerializeField] private UGUIInfoPopupWidgetView m_InfoPopupWidgetViewPrefab;
    
    private IPopupManager m_PopupManager;
    private IPopupManager PopupManager => m_PopupManager ??= Z.Get<IPopupManager>();

    private UGUIInfoPopupWidgetView m_InfoPopupWidgetView;
    
    private void Start()
    {
        PopupManager.InfoPopupWidgetProp.ValueChanged += InfoPopupWidgetProp_OnValueChanged;
    }

    private void OnDestroy()
    {
        PopupManager.InfoPopupWidgetProp.ValueChanged -= InfoPopupWidgetProp_OnValueChanged;
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
