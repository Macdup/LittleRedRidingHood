﻿using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;

public enum CreationMode // your custom enumeration
{
    Screen,
    LevelDesign,
    Artist
};

[CustomEditor(typeof(WorldMap))]
public class WorldMapEditor : Editor {

    public WorldMap map;
    static GameObject Tile;
    static CreationMode creationMode = CreationMode.Screen;
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
        //map.mapSize = EditorGUILayout.Vector2Field("Map Size:", map.mapSize);

        creationMode = (CreationMode)EditorGUILayout.EnumPopup("Creation mode:", creationMode);

        if (creationMode == CreationMode.LevelDesign) {
			Tile = (GameObject)Resources.Load("Prefabs/Environment/Tiles/ForestGround/ForestGround_Center");
           if (GUILayout.Button("Update Tiles Visu"))
            {
                updateTileVisu();
            }
				
        }
        else if (creationMode == CreationMode.Artist)
        {
            if (GUILayout.Button("Search in Cave Zone"))
            {
                int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
                EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, "BackGround_Cave", controlID);
            }

			if (GUILayout.Button("Search in Forest Zone"))
			{
				int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
				EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, "Forest", controlID);
			}

            string commandName = Event.current.commandName;
            if (commandName == "ObjectSelectorUpdated")
            {
                var currentObject = EditorGUIUtility.GetObjectPickerObject();
                Repaint();
            }
            else if (commandName == "ObjectSelectorClosed")
            {
               Tile = (GameObject)EditorGUIUtility.GetObjectPickerObject();
            }

            Tile = (GameObject)EditorGUILayout.ObjectField(Tile, typeof(GameObject),false);
        }

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

        if (Event.current.type == EventType.mouseUp && Event.current.button == 0
			|| Event.current.shift)
        {
            CreateScreenBrush();
        }

		if (Event.current.alt) {
			DeleteTile ();
		}

    }


    void CreateScreenBrush() {
        switch (creationMode)
        {
            case CreationMode.Screen:
                CreateScreen();
                break;
            case CreationMode.LevelDesign:
                CreateTile();
                break;
            case CreationMode.Artist:
                CreateObject();
                break;
        }
        
    }

    void CreateScreen() {
        var go = new GameObject("Screen");
        Undo.RegisterCreatedObjectUndo(go, "Created go");
        go.transform.SetParent(map.transform);
        go.AddComponent<Brush>();
        go.GetComponent<Brush>().brushSize = map.BrushFeedback.brushSize;
        go.AddComponent<Screen>();
        go.transform.position = map.BrushFeedback.transform.position;
		go.GetComponent<Screen> ().minBound = screenFeedbackMinBound;
		go.GetComponent<Screen> ().maxBound = screenFeedbackMaxBound;
    }

    void CreateTile() {
		var zone = getZone ();
		var tile = getTile ();
		if (zone != null && tile == null) {
			GameObject go = PrefabUtility.InstantiatePrefab(Tile,SceneManager.GetActiveScene()) as GameObject;
            Undo.RegisterCreatedObjectUndo(go, "Created go");
            go.GetComponent<Tile>().position = tileFeedbackPos;
			go.transform.SetParent (zone.transform);
			go.transform.position = map.BrushFeedback.transform.position;
		}
    }

    void CreateObject()
    {
        var zone = getZone();
        var tileOccupied = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseHitPos, -Vector2.up, 10);
        if (hits.Length != 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
				if ((hit.transform.tag == "Props" && Tile.transform.tag == "Props") || (hit.transform.tag == "Background" && Tile.transform.tag == "Background"))
                    tileOccupied = true;
            }
        }

        if (zone != null && tileOccupied == false)
        {
			GameObject go = PrefabUtility.InstantiatePrefab(Tile,SceneManager.GetActiveScene()) as GameObject;
            Undo.RegisterCreatedObjectUndo(go, "Created go");
            go.transform.SetParent(zone.transform);
            go.transform.position = new Vector3(map.BrushFeedback.transform.position.x,
                                               map.BrushFeedback.transform.position.y,
                                               map.BrushFeedback.transform.position.z);
        }
    }

    void DeleteTile()
    {
       
        if (creationMode == CreationMode.LevelDesign)
        {
            var zone = getZone();
            var tile = getTile();
            if (zone != null && tile != null)
            {
                DestroyImmediate(tile.gameObject);
            }
        }

        if (creationMode == CreationMode.Artist)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(mouseHitPos, -Vector2.up, 10);
            if (hits.Length != 0) {
                foreach (RaycastHit2D hit in hits) {
                    if (hit.transform.tag == "Props") {
                        DestroyImmediate(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    void updateBrushSize() {
        
        if (map.BrushFeedback == null)
            return;

        switch (creationMode)
        {
            case CreationMode.Screen:
                map.BrushFeedback.brushSize = new Vector2(540, 330);
                break;
            case CreationMode.LevelDesign:
                map.BrushFeedback.brushSize = new Vector2(30, 30);
                break;
            case CreationMode.Artist:
			SpriteRenderer renderer = Tile.GetComponent<SpriteRenderer>();
				if (renderer != null) {
					map.BrushFeedback.brushSize = renderer.sprite.bounds.size;
                }
                else
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

		if (p.Raycast (ray, out dist)) 
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
					y += map.transform.position.y + tileSize / 2;
                    screenFeedbackMinBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 5);
                    screenFeedbackMaxBound = new Vector2(tileFeedbackPos.x + 9, tileFeedbackPos.y - 5);
                    break;

                case CreationMode.LevelDesign:
                    x += map.transform.position.x + tileSize/ 2;
					y += map.transform.position.y + tileSize / 2;
                    break;

                case CreationMode.Artist:
				SpriteRenderer renderer = Tile.GetComponent<SpriteRenderer> ();
					if ((renderer.bounds.size.x / 30f) % 2 != 0) {
						x += map.transform.position.x + tileSize / 2;
					} else
						x += map.transform.position.x + tileSize;
					
					if ((Mathf.CeilToInt(renderer.bounds.size.y) / 30f) % 2 != 0) {
						y += map.BrushFeedback.brushSize.y/2;
					} else
						y += map.transform.position.y + tileSize;
                    break;
            }
            

            map.BrushFeedback.transform.position = new Vector3(x, y, map.transform.position.z);
        }  
    }

	Screen getZone(){
		var zoneList = map.GetComponentsInChildren<Screen> ();
		for (var i = 0; i < zoneList.Length; i++) {
			var screen = zoneList[i].GetComponent<Screen> ();
			if (tileFeedbackPos.x >= screen.minBound.x && tileFeedbackPos.y <= screen.minBound.y
			   && tileFeedbackPos.x <= screen.maxBound.x && tileFeedbackPos.y >= screen.maxBound.y) {
				return screen;
			}
		}
		return null;
	}

	Tile getTile(){
		var tileList = map.GetComponentsInChildren<Tile> ();
		for (var i = 0; i < tileList.Length; i++) {
			if (tileList[i].position == tileFeedbackPos)
				return tileList [i];
		}
		return null;
	}

	public void updateTileVisu() {
		//Je récupère l’ensemble des tiles du niveau.
		var tileList = map.GetComponentsInChildren<Tile>();
		int layerMask = 1 << LayerMask.NameToLayer ("Walls");
		//Pour chacune, je vérifie si elle a des voisins (lancé de rayon en haut, en bas, à gauche à droite) avec pour longueur la largeur d’une tile. Les objets rencontrés sont stockés au niveau de la tile elle - même
		foreach (Tile tile in tileList) {
			tileAroundDetection (tile, Vector2.up, layerMask);
			tileAroundDetection (tile, Vector2.right, layerMask);
			tileAroundDetection (tile, Vector2.down, layerMask);
			tileAroundDetection (tile, Vector2.left, layerMask);
		}
		//En fonction de ses voisins, je définis de quel type de voisin il s’agit.Voir plus la classification.
		foreach(Tile tile in tileList){
			SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer> ();
			Screen screen = tile.GetComponentInParent<Screen>();
			GameObject prefab;

			if (screen != null) {

				if (tile.Up && tile.Right && tile.Down && tile.Left) {
					//Assigner la texture centre à la tile
					if (screen != null) {
						prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_Center");
						createAuthoringInstanceFromPrefab (prefab, tile, global::Tile.Type.Center);
					}
				} else if (tile.Up && tile.Right && tile.Left) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_South");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.South);
				} else if (tile.Up && tile.Right && tile.Down) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_West");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.West);
				} else if (tile.Up && tile.Right) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_SouthWest");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.SouthWest);
				} else if (tile.Up && tile.Down && tile.Left) {
					//Assigner la texture centre à la tile*
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_East");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.East);
				} else if (tile.Up && tile.Left) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_SouthEast");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.SouthEast);
				} else if (tile.Up && tile.Down) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_WestEast");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.WestEast);
				} else if (tile.Up) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_SouthWestEast");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouthWestEast);
				} else if (tile.Right && tile.Down && tile.Left) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_North");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.North);
				} else if (tile.Down && tile.Left) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_NorthEast");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthEast);
				} else if (tile.Down && !tile.Left && !tile.Up && !tile.Right) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_NorthWestEast");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthWestEast);
				} else if (tile.Left && !tile.Right && !tile.Up && !tile.Down) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_NorthSouthEast");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouthEast);
				} else if (tile.Right && !tile.Left && !tile.Up && !tile.Down) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_NorthSouthWest");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouthWest);
				} else if (tile.Right && tile.Left && !tile.Up && !tile.Down) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_NorthSouth");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouth);
				} else if (tile.Right && tile.Down && !tile.Up && !tile.Left) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_NorthWest");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthWest);
				} else if (!tile.Right && !tile.Down && !tile.Up && !tile.Left) {
					//Assigner la texture centre à la tile
					prefab = getPrefabFromScreenZoneAndTileName (tile.zone, "_NorthSouthWestEast");
					createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouthWestEast);
				}
			}
        }

		foreach (Tile tile in tileList) {
			DestroyImmediate (tile.gameObject);
		}
		//En fonction de la zone à laquelle elle appartient, j’applique la texture correspondante.
		//To Do after the proto
		//Il faudrait construire le path vers la texture sur laquelle taper.Et si ce n’est pas une bordure, supprimer les composants non nécessaires dessus
	}

	void tileAroundDetection(Tile tile, Vector2 direction, LayerMask layerMask){
		Vector3 origin = tile.transform.position;
		if (direction == Vector2.up) {
			origin.y += 16;
			RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up,0,layerMask);
			if (hit.transform != null){
				tile.Up = hit.transform.GetComponent<Tile> ();	
			}
		}
		else if (direction == Vector2.right) {
			origin.x += 16;
			RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right,0,layerMask);
			if (hit.transform != null){
				tile.Right = hit.transform.GetComponent<Tile> ();	
			}
		}
		else if (direction == Vector2.down) {
			origin.y -= 16;
			RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down,0,layerMask);
			if (hit.transform != null){
				tile.Down = hit.transform.GetComponent<Tile> ();	
			}
		}
		else if (direction == Vector2.left) {
			origin.x -= 16;
			RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.left,0,layerMask);
			if (hit.transform != null){
				tile.Left = hit.transform.GetComponent<Tile> ();	
			}
		}
	}

	GameObject getPrefabFromScreenZoneAndTileName(Tile.Zone tileZone, string tileName){
		GameObject tilePrefab = Resources.Load<GameObject>("Prefabs/Environment/Tiles/" 
			+ tileZone.ToString() +
			"/" +
			tileZone.ToString() + tileName) as GameObject;
		return tilePrefab;
	}

	GameObject createAuthoringInstanceFromPrefab(GameObject prefab, Tile tile, Tile.Type type){
		GameObject instance = PrefabUtility.InstantiatePrefab(prefab,SceneManager.GetActiveScene()) as GameObject;
		instance.transform.position = tile.transform.position;
		instance.transform.parent = tile.transform.parent;
		Tile instanceTile = instance.GetComponent<Tile> ();
		instanceTile.position = tile.position;
		instanceTile.type = type;
		if (tile.transform.parent.GetComponent<Screen> () == null)
			instance.GetComponent<BoxCollider2D> ().isTrigger = true;
		return instance;
	}
	
}
