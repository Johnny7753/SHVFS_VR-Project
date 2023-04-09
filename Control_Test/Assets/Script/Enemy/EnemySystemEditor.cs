using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine;

[CustomPropertyDrawer(typeof(Part))]
public class EnemySystemEditor : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var switchTypeRect = new Rect(position.x, position.y, 90, position.height);
        var enemyTypeRect = new Rect(position.x + 95, position.y, 90, position.height);
        var enemyNumRect = new Rect(position.x + 190, position.y, 50, position.height);
        var switchTimeRect = new Rect(position.x + 245, position.y, 50, position.height);
        var leftEnemyRect = new Rect(position.x + 300, position.y, position.width - 50, position.height);

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(switchTypeRect, property.FindPropertyRelative("switchType"), GUIContent.none);
        EditorGUI.PropertyField(enemyTypeRect, property.FindPropertyRelative("enemyType"), GUIContent.none);
        EditorGUI.PropertyField(enemyNumRect, property.FindPropertyRelative("enemyNum"), GUIContent.none);
        EditorGUI.PropertyField(switchTimeRect, property.FindPropertyRelative("leftEnemy"), GUIContent.none);
        EditorGUI.PropertyField(leftEnemyRect, property.FindPropertyRelative("switchTime"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
