using UnityEngine;
using UnityEditor;
// using Schmarni.config;
using Schmarni.input;

[CustomEditor(typeof(inputManager))]
public class configEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        inputManager tar = (inputManager)target;
        if (GUILayout.Button("save settings"))
        { 
            tar.configManager.SaveData();
        }
    }
}