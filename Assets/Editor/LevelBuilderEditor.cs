using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelBuilder))]
public class LevelBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelBuilder levelBuilder = (LevelBuilder)target;

        if(GUILayout.Button("Build Level"))
        {
            levelBuilder.Build();
        }

    }
}
