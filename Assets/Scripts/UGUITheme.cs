using System;
using UnityEngine;

namespace Login
{
    [CreateAssetMenu]
    public sealed class UGUITheme : ScriptableObject
    {
        [SerializeField] private TabStyle m_TabStyle;

        public TabStyle TabStyle => m_TabStyle;
    }

    [Serializable]
    public sealed class TabStyle
    {
        [SerializeField] private Color m_DefaultColor = Color.white;
        [SerializeField] private Color m_SelectedColor = Color.grey;

        public Color DefaultColor => m_DefaultColor;
        public Color SelectedColor => m_SelectedColor;
    } 
}