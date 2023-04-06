using Login;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public sealed class UGUIToggleWidgetView : UGUIWidgetView<IToggleWidget>
{
    [SerializeField] private Button m_Button;
    [SerializeField] private Image m_IconImage;
    [SerializeField] private Sprite m_OnIcon;
    [SerializeField] private Sprite m_OffIcon;

    protected override void OnBindToModel(IToggleWidget model)
    {
        base.OnBindToModel(model);
        Bind(model.IsOnProp, isOn =>
        {
            if (isOn)
                m_IconImage.sprite = m_OnIcon;
            else
                m_IconImage.sprite = m_OffIcon;
        });
        Bind(model.IsInteractableProperty, value => m_Button.interactable = value);
        Bind(model.IsFocusedProperty, UpdateFocusState);
        
        m_Button.onClick.AddListener(Button_OnClicked);
    }

    protected override void OnUnbindFromModel(IToggleWidget model)
    {
        m_Button.onClick.RemoveListener(Button_OnClicked);
        base.OnUnbindFromModel(model);
    }

    private void UpdateFocusState(bool isFocused)
    {
        if (EventSystem.current != null && !EventSystem.current.alreadySelecting)
            EventSystem.current.SetSelectedGameObject(m_Button.gameObject);
    }

    private void Button_OnClicked()
    {
        Model.HandleClick();
    }
}