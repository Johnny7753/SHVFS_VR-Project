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
        EditorGUI.BeginChangeCheck();
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
        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();
    }
    //SerializedProperty waveIndex;
    //SerializedProperty partIndex;
    //SerializedProperty enemyAlive;
    //SerializedProperty bornBoundary;
    //SerializedProperty freshTimeInterval;
    //SerializedProperty waves;
    //SerializedProperty enemyPrefabs;

    //private void OnEnable()
    //{
    //    waveIndex = serializedObject.FindProperty("waveIndex");
    //    partIndex = serializedObject.FindProperty("partIndex");
    //    enemyAlive = serializedObject.FindProperty("enemyAlive");
    //    bornBoundary = serializedObject.FindProperty("bornBoundary");
    //    freshTimeInterval = serializedObject.FindProperty("freshTimeInterval");
    //    waves = serializedObject.FindProperty("waves");
    //    enemyPrefabs = serializedObject.FindProperty("enemyPrefabs");
    //}
    //public override void OnInspectorGUI()
    //{
    //    EnemySystem t = (EnemySystem)target;

    //    EditorGUILayout.LabelField("Enemy Information");
    //    EditorGUILayout.PropertyField(waveIndex);
    //    EditorGUILayout.PropertyField(partIndex);
    //    EditorGUILayout.PropertyField(enemyAlive);

    //    EditorGUILayout.Space();
    //    EditorGUILayout.Space();

    //    EditorGUILayout.LabelField("Enemy Spawn");
    //    EditorGUILayout.PropertyField(bornBoundary);
    //    EditorGUILayout.PropertyField(freshTimeInterval);
    //    if(t.waves[].parts[partIndex].switchType==SwitchType.EnemyLeft)
    //    {

    //    }
    //    ///Part t = target as Part;
    //    //if (t.switchType==SwitchType.EnemyLeft)
    //    //{
    //    //    EditorGUILayout.IntField("leftEnemy", t.leftEnemy);
    //    //}
    //    //else
    //    //{
    //    //    EditorGUILayout.IntField("switchTime", t.switchTime);
    //    //}
    //}
}
