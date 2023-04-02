using Login;
using TMPro;
using UnityEngine;

public sealed class UGUITextInputWidgetView : UGUIWidgetView<ITextInputWidget>
{
    [SerializeField] private TMP_InputField m_InputField;
        
    private TMP_InputField.ContentType m_CachedContentType;

    protected override void Awake()
    {
        base.Awake();
        m_CachedContentType = m_InputField.contentType;
    }

    protected override void OnBindToModel(ITextInputWidget model)
    {
        base.OnBindToModel(model);
        Bind(model.TextProp, m_InputField.SetTextWithoutNotify);
        Bind(model.IsInteractableProperty, UpdateInteractableState);
        Bind(model.IsMaskingCharactersProperty, UpdateCharacterMaskingState);
        Bind(model.IsFocused, UpdateFocusedState);
        
        m_InputField.onSelect.AddListener(InputField_OnSelected);
        m_InputField.onDeselect.AddListener(InputField_OnDeselected);
        m_InputField.onValueChanged.AddListener(InputField_OnValueChanged);
    }

    private void UpdateFocusedState(bool isFocused)
    {
        if (isFocused)
            m_InputField.Select();
    }

    private void InputField_OnSelected(string value)
    {
        Model.IsFocused.Set(true);
    }
    
    private void InputField_OnDeselected(string value)
    {
        Model.IsFocused.Set(false);
    }

    protected override void OnUnbindFromModel(ITextInputWidget model)
    {
        m_InputField.onValueChanged.RemoveListener(InputField_OnValueChanged);
        base.OnUnbindFromModel(model);
    }

    private void UpdateInteractableState(bool isInteractable)
    {
        m_InputField.interactable = isInteractable;
    }

    private void UpdateCharacterMaskingState(bool isMasking)
    {
        if (isMasking)
            m_InputField.contentType = TMP_InputField.ContentType.Password;
        else
            m_InputField.contentType = m_CachedContentType;
            
        m_InputField.ForceLabelUpdate();
    }

    private void InputField_OnValueChanged(string value)
    {
        Model.TextProp.Set(value);
    }
}