using System;
using UnityEngine;

[CreateAssetMenu]
public sealed class UGUITheme : ScriptableObject
{
    [SerializeField] private Color m_ErrorColor = Color.red;
    [SerializeField] private Color m_HighlightColor = Color.green;
    [SerializeField] private TabStyle m_TabStyle;

    public TabStyle TabStyle => m_TabStyle;
    public Color ErrorColor => m_ErrorColor;
    public Color HighlightColor => m_HighlightColor;
}

[Serializable]
public sealed class TabStyle
{
    [SerializeField] private Color m_DefaultColor = Color.white;
    [SerializeField] private Color m_SelectedColor = Color.grey;

    public Color DefaultColor => m_DefaultColor;
    public Color SelectedColor => m_SelectedColor;
}