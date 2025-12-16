using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ARTarget))]
public class ARTargetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty listProp = serializedObject.FindProperty("arTargetList");
        SerializedProperty indexProp = serializedObject.FindProperty("selectedTargetIndex");

        EditorGUILayout.PropertyField(listProp);

        ARTargetList list = listProp.objectReferenceValue as ARTargetList;
        if (list != null)
        {
            int count = list.Targets != null ? list.Targets.Count : 0;
            string[] options = new string[count];
            for (int i = 0; i < count; i++)
            {
                var t = list.Targets[i];
                options[i] = t != null ? t.name : $"Null ({i})";
            }

            // Clamp previously stored index
            if (indexProp.intValue >= count) indexProp.intValue = count - 1;
            if (indexProp.intValue < -1) indexProp.intValue = -1;

            indexProp.intValue = EditorGUILayout.Popup("Selected Target", Mathf.Max(0, indexProp.intValue), options);
            if (count == 0)
            {
                EditorGUILayout.HelpBox("The ARTargetList has no entries.", MessageType.Warning);
                indexProp.intValue = -1;
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Assign an ARTargetList to choose a target from.", MessageType.Info);
            indexProp.intValue = -1;
        }

        // Draw remaining default properties
        DrawPropertiesExcluding(serializedObject, "arTargetList", "selectedTargetIndex");
        serializedObject.ApplyModifiedProperties();
    }
}