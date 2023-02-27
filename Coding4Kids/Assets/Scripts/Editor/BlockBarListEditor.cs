using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.TerrainTools;

[CustomEditor(typeof(BlockBarItemList))]
public class BlockBarListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("items"));
        serializedObject.ApplyModifiedProperties();
    }

    public static void Show(SerializedProperty list)
    {
        EditorGUILayout.PropertyField(list);
        for(int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
        }
    }    
}
