using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[CanEditMultipleObjects]
[CustomEditor(typeof(Screen))]
public class ScreenEditor : Editor
{
	Vector2 centerPos;

    public override void OnInspectorGUI()
    {
		Screen screen = target as Screen;

        EditorGUILayout.BeginVertical();
        if (GUILayout.Button("Move Zone(s) up"))
        {
            moveZonesUp();
        }
        if (GUILayout.Button("Move Zone(s) down"))
        {
            moveZonesDown();
        }
        if (GUILayout.Button("Move Zone(s) right"))
        {
            moveZonesRight();
        }
        if (GUILayout.Button("Move Zone(s) left"))
        {
            moveZonesLeft();
        }

		EditorGUI.BeginChangeCheck ();
		screen.zone = (Screen.Zone)EditorGUILayout.EnumPopup("Zone :", screen.zone);
		if (EditorGUI.EndChangeCheck ()) {
			foreach(Object obj in targets){ 
				((Screen)obj).zone = screen.zone;
			}
		}

		if (GUILayout.Button("Update Tile Screen"))
		{
			updateTileVisu();
		}
        EditorGUILayout.EndVertical();
    }

    public void moveZonesUp()
    {
        var selectionList = Selection.transforms;
        for (var i = 0; i < selectionList.Length; i++) {
            Vector3 newPos = selectionList[i].transform.position;
            newPos.y += 330;
            selectionList[i].transform.position = newPos;
			var tileFeedbackPos = new Vector2(Mathf.Floor(newPos.x / 30), Mathf.Floor(newPos.y / 30) + 1);
			var screen = selectionList [i].GetComponent<Screen> ();
			screen.minBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 5);
			screen.maxBound = new Vector2(tileFeedbackPos.x + 8, tileFeedbackPos.y - 5);
			Tile[] tiles = selectionList [i].GetComponentsInChildren<Tile> ();
			foreach (Tile tile in tiles) {
				calculateTilesPosition (tile);
			}
        }
    }

    public void moveZonesDown()
    {
        var selectionList = Selection.transforms;
        for (var i = 0; i < selectionList.Length; i++)
        {
            Vector3 newPos = selectionList[i].transform.position;
            newPos.y -= 330;
            selectionList[i].transform.position = newPos;
			var tileFeedbackPos = new Vector2(Mathf.Floor(newPos.x / 30), Mathf.Floor(newPos.y / 30) + 1);
			var screen = selectionList [i].GetComponent<Screen> ();
			screen.minBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 5);
			screen.maxBound = new Vector2(tileFeedbackPos.x + 8, tileFeedbackPos.y - 5);
			Tile[] tiles = selectionList [i].GetComponentsInChildren<Tile> ();
			foreach (Tile tile in tiles) {
				calculateTilesPosition (tile);
			}
        }
    }

    public void moveZonesRight()
    {
        var selectionList = Selection.transforms;
        for (var i = 0; i < selectionList.Length; i++)
        {
            Vector3 newPos = selectionList[i].transform.position;
            newPos.x += 540;
            selectionList[i].transform.position = newPos;
			var tileFeedbackPos = new Vector2(Mathf.Floor(newPos.x / 30), Mathf.Floor(newPos.y / 30) + 1);
			var screen = selectionList [i].GetComponent<Screen> ();
			screen.minBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 5);
			screen.maxBound = new Vector2(tileFeedbackPos.x + 8, tileFeedbackPos.y - 5);
			Tile[] tiles = selectionList [i].GetComponentsInChildren<Tile> ();
			foreach (Tile tile in tiles) {
				calculateTilesPosition (tile);
			}
        }
    }

    public void moveZonesLeft()
    {
        var selectionList = Selection.transforms;
        for (var i = 0; i < selectionList.Length; i++)
        {
            Vector3 newPos = selectionList[i].transform.position;
            newPos.x -= 540;
            selectionList[i].transform.position = newPos;
			var tileFeedbackPos = new Vector2(Mathf.Floor(newPos.x / 30), Mathf.Floor(newPos.y / 30) + 1);
			var screen = selectionList [i].GetComponent<Screen> ();
			screen.minBound = new Vector2(tileFeedbackPos.x - 8, tileFeedbackPos.y + 5);
			screen.maxBound = new Vector2(tileFeedbackPos.x + 8, tileFeedbackPos.y - 5);
			Tile[] tiles = selectionList [i].GetComponentsInChildren<Tile> ();
			foreach (Tile tile in tiles) {
				calculateTilesPosition (tile);
			}
        }
    }

	void calculateTilesPosition(Tile tile){
		tile.position = new Vector2 (tile.transform.position.x / 30 - 0.5f,tile.transform.position.y / 30 + 0.5f);
	}

	public void updateTileVisu() {
		//Je récupère l’ensemble des tiles du niveau.
		var selectionList = Selection.transforms;
		foreach (Transform screenTransform in selectionList) {
			var tileList = screenTransform.GetComponentsInChildren<Tile>();
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
								prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_Center");
								createAuthoringInstanceFromPrefab (prefab, tile, global::Tile.Type.Center);
							}
						} else if (tile.Up && tile.Right && tile.Left) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_South");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.South);
						} else if (tile.Up && tile.Right && tile.Down) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_West");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.West);
						} else if (tile.Up && tile.Right) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_SouthWest");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.SouthWest);
						} else if (tile.Up && tile.Down && tile.Left) {
							//Assigner la texture centre à la tile*
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_East");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.East);
						} else if (tile.Up && tile.Left) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_SouthEast");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.SouthEast);
						} else if (tile.Up && tile.Down) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_WestEast");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.WestEast);
						} else if (tile.Up) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_SouthWestEast");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouthWestEast);
						} else if (tile.Right && tile.Down && tile.Left) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_North");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.North);
						} else if (tile.Down && tile.Left) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_NorthEast");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthEast);
						} else if (tile.Down && !tile.Left && !tile.Up && !tile.Right) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_NorthWestEast");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthWestEast);
						} else if (tile.Left && !tile.Right && !tile.Up && !tile.Down) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_NorthSouthEast");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouthEast);
						} else if (tile.Right && !tile.Left && !tile.Up && !tile.Down) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_NorthSouthWest");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouthWest);
						} else if (tile.Right && tile.Left && !tile.Up && !tile.Down) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_NorthSouth");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouth);
						} else if (tile.Right && tile.Down && !tile.Up && !tile.Left) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_NorthWest");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthWest);
						} else if (!tile.Right && !tile.Down && !tile.Up && !tile.Left) {
							//Assigner la texture centre à la tile
							prefab = getPrefabFromScreenZoneAndTileName (screen.zone, "_NorthSouthWestEast");
							createAuthoringInstanceFromPrefab (prefab, tile,global::Tile.Type.NorthSouthWestEast);
						}
				}
			}

			foreach (Tile tile in tileList) {
				DestroyImmediate (tile.gameObject);
			}
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

	GameObject getPrefabFromScreenZoneAndTileName(Screen.Zone screenZone, string tileName){
		GameObject tilePrefab = Resources.Load<GameObject>("Prefabs/Environment/Tiles/" 
			+ screenZone.ToString() +
			"/" +
			screenZone.ToString() + tileName) as GameObject;
		return tilePrefab;
	}

	GameObject createAuthoringInstanceFromPrefab(GameObject prefab, Tile tile, Tile.Type type){
		GameObject instance = PrefabUtility.InstantiatePrefab(prefab,SceneManager.GetActiveScene()) as GameObject;
		instance.transform.position = tile.transform.position;
		instance.transform.parent = tile.transform.parent;
		Tile instanceTile = instance.GetComponent<Tile> ();
		instanceTile.position = tile.position;
		instanceTile.type = type;
		Debug.Log (instanceTile.type);
		if (tile.transform.parent.GetComponent<Screen> () == null)
			instance.GetComponent<BoxCollider2D> ().isTrigger = true;
		return instance;
	}


}