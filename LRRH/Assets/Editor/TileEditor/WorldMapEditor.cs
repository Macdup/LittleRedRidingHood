using UnityEngine;
using System.Collections;
using UnityEditor;

public enum CreationMode // your custom enumeration
{
    Screen,
    Tile
};

[CustomEditor(typeof(WorldMap))]
public class WorldMapEditor : Editor {

    public WorldMap map;
    public CreationMode creationMode = CreationMode.Screen;
    Brush brush;
    Vector3 mouseHitPos;
    Vector2 tileFeedbackPos;
    Vector2 screenFeedbackMinBound;
    Vector2 screenFeedbackMaxBound;


    bool mouseOnMap {
        get
        {return mouseHitPos.x > 0 && mouseHitPos.x < map.gridSize.x && mouseHitPos.y < 0 && mouseHitPos.y > -map.gridSize.y;}
    }

    bool screenFeedbackOnMap{
        get
        {
            return mouseHitPos.x - map.BrushFeedback.brushSize.x/2 + 30 > 0 && mouseHitPos.x + map.BrushFeedback.brushSize.x/2 < map.gridSize.x
                   && mouseHitPos.y + map.BrushFeedback.brushSize.y/2 < 0 && mouseHitPos.y - map.BrushFeedback.brushSize.y/2 > -map.gridSize.y;
        }
    }
   

    public override void OnInspectorGUI(){
        EditorGUILayout.BeginVertical();

        var oldSize = map.mapSize;
        map.mapSize = EditorGUILayout.Vector2Field("Map Size:", map.mapSize);
        creationMode = (CreationMode)EditorGUILayout.EnumPopup("Creation mode:", creationMode);
        screenFeedbackMinBound = EditorGUILayout.Vector2Field("screenFeedbackMinBound:", screenFeedbackMinBound);
        tileFeedbackPos = EditorGUILayout.Vector2Field("tileFeedbackPos:", tileFeedbackPos);

        //map.BrushFeedback.brushSize

        EditorGUILayout.EndVertical();
    }

    void OnEnable() {
        map = target as WorldMap;
        Tools.current = Tool.View;
    }

    void OnDisable() {
        DestroyBrush();
        DestroyImmediate(map.BrushFeedback.transform.gameObject);
    }

    void OnSceneGUI() {
        UpdateHitPosition();
        updateBrushSize();
        MoveBrush();

        if (Event.current.type == EventType.mouseUp && Event.current.button == 0)
        {
            CreateScreenBrush();
        }   
    }


    void CreateScreenBrush() {
        switch (creationMode)
        {
            case CreationMode.Screen:
                CreateScreen();
                break;
            case CreationMode.Tile:
                CreateTile();
                break;
        }
        
    }

    void CreateScreen() {
        var go = new GameObject("Screen");
        go.transform.SetParent(map.transform);
        go.AddComponent<Brush>();
        go.GetComponent<Brush>().brushSize = map.BrushFeedback.brushSize;
        go.AddComponent<Screen>();
        go.transform.position = map.BrushFeedback.transform.position;
    }

    void CreateTile() {
        var go = new GameObject("Tile");
        go.transform.SetParent(map.transform);
        go.AddComponent<Brush>();
        go.GetComponent<Brush>().brushSize = map.BrushFeedback.brushSize;
        go.AddComponent<Tile>();
        go.transform.position = map.BrushFeedback.transform.position;
    }

    void updateBrushSize() {
        
        if (map.BrushFeedback == null)
            return;

        switch (creationMode)
        {
            case CreationMode.Screen:
                map.BrushFeedback.brushSize = new Vector2(540, 330);
                break;
            case CreationMode.Tile:
                map.BrushFeedback.brushSize = new Vector2(30, 30);
                break;
        }
    }


    void DestroyBrush() {
        if (brush != null)
            DestroyImmediate(brush);
    }


    void UpdateHitPosition(){
        var p = new Plane(map.transform.TransformDirection(Vector3.forward),Vector3.zero);
        var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        var hit = Vector3.zero;
        var dist = 0.0f;

        if(p.Raycast(ray, out dist))
            hit = ray.origin + ray.direction.normalized  * dist;

        mouseHitPos = map.transform.InverseTransformDirection(hit);
    }

    void MoveBrush() {
        if (map.BrushFeedback != null) {
            var tileSize = map.tileSize.x;
            var x = Mathf.Floor(mouseHitPos.x / tileSize) * tileSize;
            var y = Mathf.Floor(mouseHitPos.y / tileSize) * tileSize;
            tileFeedbackPos = new Vector2(Mathf.Floor(mouseHitPos.x / tileSize), Mathf.Floor(mouseHitPos.y / tileSize) + 1);

            switch (creationMode)
            {
                case CreationMode.Screen:
                    x += map.transform.position.x + tileSize;
                    screenFeedbackMinBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 6);
                    screenFeedbackMinBound = new Vector2(tileFeedbackPos.x + 8, tileFeedbackPos.y - 5);
                    break;
                case CreationMode.Tile:
                    x += map.transform.position.x + tileSize/ 2;
                    break;
            }
            
            y += map.transform.position.y + tileSize / 2;

            map.BrushFeedback.transform.position = new Vector3(x, y, map.transform.position.z);
        }  
    }
	
}
