using System;
using Common.Widgets;
using UGUI;
using UnityEngine;

namespace Bindings
{
    public sealed class UGUITextFieldWidgetViewBinding : MonoBehaviour
    {
        [SerializeField] private UGUITextFieldWidgetView m_TextFieldWidgetView;
        [SerializeField] private string m_WidgetId;

        private void Awake()
        {
            var provider = GetComponentInParent<IWidgetProvider>();
            m_TextFieldWidgetView.Model = provider.Get<ITextFieldWidget>(m_WidgetId);
        }
    }

    public interface IWidgetProvider
    {
        public TWidget Get<TWidget>(string widgetId) where TWidget : class, IWidget;
    }
}
