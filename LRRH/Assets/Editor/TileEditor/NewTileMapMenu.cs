using UnityEngine;
using System.Collections;
using UnityEditor;

public class NewTileMapMenu : MonoBehaviour {

    [MenuItem("Level Editor/Tile Map")]
    public static void CreateTileMap() {
        GameObject go = new GameObject("Tile Map");
        go.AddComponent<TileMap>();
    }
}
