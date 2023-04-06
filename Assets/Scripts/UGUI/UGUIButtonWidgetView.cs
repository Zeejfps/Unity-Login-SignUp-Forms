using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UGUI
{
    public sealed class UGUIButtonWidgetView : UGUIWidgetView<IButtonWidget>
    {
        [SerializeField] private Button m_Button;
        [SerializeField] private TMP_Text m_ButtonLabel;
        [SerializeField] private GameObject m_LoadingIndicatorWidgetView;

        protected override void OnBindToModel(IButtonWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.IsInteractableProperty, value => m_Button.interactable = value);
            Bind(model.IsLoadingProp, UpdateIsLoadingState);
            Bind(model.IsFocusedProperty, UpdateFocusState);
            m_Button.onClick.AddListener(Button_OnClicked);
        }

        private void UpdateIsLoadingState(bool isLoading)
        {
            m_ButtonLabel.gameObject.SetActive(!isLoading);
            m_LoadingIndicatorWidgetView.SetActive(isLoading);
        }
        
        private void UpdateFocusState(bool isFocused)
        {
            if (EventSystem.current != null && !EventSystem.current.alreadySelecting)
                EventSystem.current.SetSelectedGameObject(m_Button.gameObject);
        }

        protected override void OnUnbindFromModel(IButtonWidget model)
        {
            m_Button.onClick.RemoveListener(Button_OnClicked);
            base.OnUnbindFromModel(model);
        }

        private void Button_OnClicked()
        {
            Model.ActionProp.Value?.Invoke();
        }
    }
}