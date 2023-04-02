using TMPro;
using UnityEngine;

namespace Login
{
    public sealed class UGUIPasswordRequirementWidgetView : UGUIWidgetView<IPasswordRequirementWidget>
    {
        [SerializeField] private TMP_Text m_Text;

        protected override void OnBindToModel(IPasswordRequirementWidget model)
        {
            base.OnBindToModel(model);
            Bind(model.Description, value =>
            {
                m_Text.text = value;
            });
        }
    }
}