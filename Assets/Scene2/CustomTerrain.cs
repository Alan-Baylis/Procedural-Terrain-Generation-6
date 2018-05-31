using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
    [ExecuteInEditMode]
public class CustomTerrain : MonoBehaviour {

    public Vector2 randomHeightRange = new Vector2(0, 0.1f);
    public Texture2D heightMapImage;
    public Vector3 heightMapScale = Vector3.one; 

    public Terrain terrain;
    public TerrainData terrainData;

    public void RandomTerrain() {
        float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
        for (int x = 0; x < terrainData.heightmapWidth; x++) {
            for (int z = 0; z < terrainData.heightmapHeight; z++) {
                heightMap[x, z] += Random.Range(randomHeightRange.x, randomHeightRange.y); // Adding instead of resetting
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }
    public void LoadTexture() {
        float[,] heightMap = new float[terrainData.heightmapWidth, terrainData.heightmapHeight];
        for (int x = 0; x < terrainData.heightmapWidth; x++) {
            for (int z = 0; z < terrainData.heightmapHeight; z++) {
                // Scaling in x, z our heightmap fits the resolution of the heightmapimage
                // Scaling in y just multiplies the height
                heightMap[x, z] = heightMapImage.GetPixel((int) (x * heightMapScale.x), (int) (z * heightMapScale.z)).grayscale * heightMapScale.y;  
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }






    public void ResetTerrain () {
        float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
        for (int x = 0; x < terrainData.heightmapWidth; x++) {
            for (int z = 0; z < terrainData.heightmapHeight; z++) {
                heightMap[x, z] = 0; 
            }
        }
        terrainData.SetHeights(0, 0, heightMap);
    }

    private void OnEnable() {  
        Debug.Log("Initialising Terrain Data");
        terrain = GetComponent<Terrain>();
        terrainData = Terrain.activeTerrain.terrainData;
    }
    void Awake() {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]); // Getting tag manager
        SerializedProperty tagsProp = tagManager.FindProperty("tags"); // Getting tags property of tag manager
    
        AddTag(tagsProp, "Terrain");
        AddTag(tagsProp, "Cloud");
        AddTag(tagsProp, "Shore");

        // Apply tag changes to tag database
        tagManager.ApplyModifiedProperties();

        // Take this object
        gameObject.tag = "Terrain";

    }
    void AddTag(SerializedProperty tagsProp, string newTag) {
        bool found = false;
        // Ensure the tag does not already exist
        for(int i = 0; i < tagsProp.arraySize; i++) {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if(t.stringValue.Equals(newTag)) {
                found = true;
                break;
            }
        }
        // Add your new tag
        if(!found) {
            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty newTagProp = tagsProp.GetArrayElementAtIndex(0);
            newTagProp.stringValue = newTag;
        }
    }
    private void Start() {
        
    }
    private void Update() {
    }
}
