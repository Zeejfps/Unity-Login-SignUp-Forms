using Login;
using TMPro;
using UnityEngine;
using YADBF.Unity;

public sealed class UGUITextFieldWidgetView : UGUIWidgetView<ITextFieldWidget>
{
    [SerializeField] private View<ITextInputWidget> m_TextInputWidgetView;
    [SerializeField] private TMP_Text m_ErrorText;
    [SerializeField] private GameObject m_ErrorHighlight;

    protected override void OnBindToModel(ITextFieldWidget model)
    {
        base.OnBindToModel(model);
        m_TextInputWidgetView.Model = model.TextInputWidget;
        Bind(model.ErrorTextProp, value =>
        {
            var showHighlight = !string.IsNullOrWhiteSpace(value);
            m_ErrorText.text = value;
            m_ErrorHighlight.SetActive(showHighlight);
        });
    }
}