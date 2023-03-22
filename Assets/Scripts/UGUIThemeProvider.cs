using UnityEngine;

namespace Login
{
    [CreateAssetMenu]
    public sealed class UGUIThemeProvider : ScriptableObject
    {
        [SerializeField] private UGUITheme m_Theme;
        public UGUITheme Theme => m_Theme;
    }
}