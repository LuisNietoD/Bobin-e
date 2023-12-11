using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AttackBehavior))]
public class AttackBehaviorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        EditorGUI.EndProperty();
    }
}