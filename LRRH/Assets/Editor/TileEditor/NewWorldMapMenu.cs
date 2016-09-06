using UnityEngine;
using System.Collections;
using UnityEditor;

public class NewTileMapMenu : MonoBehaviour {

    [MenuItem("Level Editor/World Map")]
    public static void CreateWorldMap() {
        GameObject go = new GameObject("World Map");
        go.AddComponent<WorldMap>();
    }
}
