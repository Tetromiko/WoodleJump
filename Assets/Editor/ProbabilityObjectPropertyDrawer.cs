using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ProbabilityObject))]
    public class ProbabilityObjectPropertyDrawer: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float halfWidth = position.width * 0.5f;

            Rect objPosition = new Rect(position.x, position.y, halfWidth, position.height);
            Rect weightPosition = new Rect(position.x + halfWidth, position.y, halfWidth, position.height);

            EditorGUI.PrefixLabel(objPosition, GUIUtility.GetControlID(FocusType.Passive), label);
            EditorGUI.PropertyField(objPosition, property.FindPropertyRelative("obj"), GUIContent.none);

            EditorGUI.PropertyField(weightPosition, property.FindPropertyRelative("weight"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}