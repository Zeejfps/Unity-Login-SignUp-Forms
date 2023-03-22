using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace YADBF.Unity.Editor
{
    [CustomPropertyDrawer(typeof(PropertyBindingSettings), true)]
    public sealed class PropertyBindingSettingsPropertyDrawer : PropertyDrawer
    {
        private const float Padding = 2f;

        private List<string> m_Options = new List<string>();
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            {
                var helperRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                var viewProperty = property.FindPropertyRelative($"m_{nameof(PropertyBindingSettings.View)}");
                var propertyNameProperty = property.FindPropertyRelative($"m_{nameof(PropertyBindingSettings.PropertyName)}");
                var propertyTypeNameProperty = property.FindPropertyRelative($"m_PropertyTypeName");
                
                EditorGUI.PropertyField(helperRect, viewProperty);
                helperRect.y += EditorGUIUtility.singleLineHeight + Padding;

                UpdateOptions(viewProperty, propertyTypeNameProperty);
                
                var selectedIndex = -1;
                var propertyName = propertyNameProperty.stringValue;
                if (propertyName != null)
                    selectedIndex = m_Options.IndexOf(propertyName);

                selectedIndex = EditorGUI.Popup(helperRect, "Property Name", selectedIndex, m_Options.ToArray());
                if (selectedIndex >= 0 && selectedIndex < m_Options.Count)
                {
                    propertyNameProperty.stringValue = m_Options[selectedIndex];
                }
                else
                {
                    propertyNameProperty.stringValue = string.Empty;
                }
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var totalHeight = 0f;

            totalHeight += EditorGUIUtility.singleLineHeight;
            totalHeight += Padding;
            totalHeight += EditorGUIUtility.singleLineHeight;

            return totalHeight;
        }

        private void UpdateOptions(
            SerializedProperty viewProperty,
            SerializedProperty propertyTypeNameProperty)
        {
            m_Options.Clear();

            var view = viewProperty.objectReferenceValue as View;
            if (view == null)
                return;

            var propertyTypeName = propertyTypeNameProperty.stringValue;
            if (propertyTypeName == null)
                return;

            var propertyType = Type.GetType(propertyTypeName);
            if (propertyType == null)
                return;

            var viewModelType = view.GetViewModelType();
            m_Options.AddRange(viewModelType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(propertyInfo => propertyInfo.PropertyType.IsAssignableFrom(propertyType))
                .Select(propertyInfo => propertyInfo.Name));
        }
    }
}
