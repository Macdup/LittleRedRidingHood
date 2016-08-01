using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor {

    public TileMap map;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        var oldSize = map.mapSize;
        map.mapSize = EditorGUILayout.Vector2Field("Map Size:", map.mapSize);
        if (map.mapSize != oldSize) {
            UpdateCalculations();
        }

        map.texture2D = (Texture2D)EditorGUILayout.ObjectField("Texture2D", map.texture2D, typeof(Texture2D), false);

        if (map.texture2D == null)
        {
            EditorGUILayout.HelpBox("You have not selected a texture 2D yet.", MessageType.Warning);
        }
        else {
            EditorGUILayout.LabelField("Tile Size", map.tileSize.x + "x" + map.tileSize.y +"y");
            EditorGUILayout.LabelField("Grid Size In Units:", map.gridSize.x + "x" + map.mapSize.y + "y");
            EditorGUILayout.LabelField("Pixels To Units:", map.pixelsToUnits.ToString());
        }

        EditorGUILayout.EndVertical();
    }

    void OnEnable() {
        map = target as TileMap;
        Tools.current = Tool.View;

        if (map.texture2D != null) {
            UpdateCalculations();
        }

    }

    void UpdateCalculations() {
        var path = AssetDatabase.GetAssetPath(map.texture2D);
        map.spriteReferences = AssetDatabase.LoadAllAssetsAtPath(path);

        var sprite = (Sprite)map.spriteReferences[1];
        var width = sprite.textureRect.width;
        var height = sprite.textureRect.height;

        map.tileSize = new Vector2(width, height);
        map.pixelsToUnits = (int)(sprite.rect.width / sprite.bounds.size.x);
        map.gridSize = new Vector2((width / map.pixelsToUnits) * map.mapSize.x, (height / map.pixelsToUnits) * map.mapSize.y);
    }
	
}
