using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EditorGUITable; // We will get this from asset store

[CustomEditor(typeof(CustomTerrain))] // Links the editor code in here with CustomTerrain class.
[CanEditMultipleObjects]

public class CustomTerrainEditor : Editor {
    //properties ------
    SerializedProperty randomHeightRange;
    SerializedProperty heightMapScale;
    SerializedProperty heightMapImage;

    //fold outs ------
    bool showRandom = false;
    bool showLoadHeights = false;

    private void OnEnable() {
        
        randomHeightRange = serializedObject.FindProperty("randomHeightRange"); // Linking the randomheightrange in the other script to this
        heightMapScale = serializedObject.FindProperty("heightMapScale");
        heightMapImage = serializedObject.FindProperty("heightMapImage");
    }
    public override void OnInspectorGUI() { // Graphical UI you see in the inspector for your custom terrain.
        serializedObject.Update(); // Updates all of the serialized values going on between this script and customterrain
        CustomTerrain terrain = (CustomTerrain) target; // Link to the CustomTerrain script, target is the object being inspected
        showRandom = EditorGUILayout.Foldout(showRandom, "Random"); // Triangle in the inspector you use to open or close
        if (showRandom) {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Set Heights Between Random Values", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(randomHeightRange);
            if (GUILayout.Button("Random Heights")) {
                terrain.RandomTerrain();
            }
        }
        showLoadHeights = EditorGUILayout.Foldout(showLoadHeights, "Load Heights");
        if (showLoadHeights) {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Load Heights From Texture", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(heightMapImage);
            EditorGUILayout.PropertyField(heightMapScale);
            if (GUILayout.Button("Load Texture")) {
                terrain.LoadTexture();
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        if (GUILayout.Button("Reset Heights")) {
            terrain.ResetTerrain();
        }

        serializedObject.ApplyModifiedProperties(); // Apply any changes that happened
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
