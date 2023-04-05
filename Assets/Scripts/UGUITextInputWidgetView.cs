using System.Collections;
using Login;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using YADBF;

public sealed class UGUITextInputWidgetView : UGUIWidgetView<ITextInputWidget>
{
    [SerializeField] private TMP_InputField m_InputField;
        
    private TMP_InputField.ContentType m_CachedContentType;

    protected override void Awake()
    {
        base.Awake();
        m_CachedContentType = m_InputField.contentType;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (Model != null && Model.IsFocusedProperty.IsTrue())
            Focus();
    }

    protected override void OnBindToModel(ITextInputWidget model)
    {
        base.OnBindToModel(model);
        Bind(model.TextProp, m_InputField.SetTextWithoutNotify);
        Bind(model.IsInteractableProperty, UpdateInteractableState);
        Bind(model.IsMaskingCharactersProperty, UpdateCharacterMaskingState);
        Bind(model.IsFocusedProperty, UpdateFocusedState);
        
        m_InputField.onSelect.AddListener(InputField_OnSelected);
        m_InputField.onDeselect.AddListener(InputField_OnDeselected);
        m_InputField.onValueChanged.AddListener(InputField_OnValueChanged);
    }

    private void UpdateFocusedState(bool isFocused)
    {
        if (isFocused)
            Focus();
    }

    private void Focus()
    {
        if (!EventSystem.current.alreadySelecting)
            EventSystem.current.SetSelectedGameObject(m_InputField.gameObject);
        m_InputField.ActivateInputField();
    }

    private void InputField_OnSelected(string value)
    {
        Model.IsFocusedProperty.Set(true);
    }
    
    private void InputField_OnDeselected(string value)
    {
        Model.IsFocusedProperty.Set(false);
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