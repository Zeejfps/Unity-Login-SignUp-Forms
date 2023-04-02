using Login;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YADBF.Unity;

[RequireComponent(typeof(RectTransform))]
public sealed class UGUITextFieldWidgetView : UGUIWidgetView<ITextFieldWidget>
{
    [SerializeField] private View<ITextInputWidget> m_TextInputWidgetView;
    [SerializeField] private TMP_Text m_ErrorText;
    [SerializeField] private GameObject m_ErrorHighlight;
    [SerializeField] private bool m_RebuildLayout = true;

    private RectTransform RectTransform { get; set; }

    protected override void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        base.Awake();
    }

    protected override void OnBindToModel(ITextFieldWidget model)
    {
        base.OnBindToModel(model);
        m_TextInputWidgetView.Model = model.TextInputWidget;
        Bind(model.ErrorTextProp, value =>
        {
            var showHighlight = !string.IsNullOrWhiteSpace(value);
            m_ErrorText.text = value;
            m_ErrorHighlight.SetActive(showHighlight);
            if (m_RebuildLayout) RebuildLayout();
        });
    }

    private void RebuildLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(m_ErrorText.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(RectTransform);

        var parent = transform.parent;
        if (parent == null)
            return;
        
        var parentLayoutGroup = parent.GetComponentInParent<LayoutGroup>();
        if (parentLayoutGroup == null)
            return;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)parentLayoutGroup.transform);
    }
}