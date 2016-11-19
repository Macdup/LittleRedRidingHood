using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
    using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour {

	public enum Zone 
	{
		ForestGround,
		CaveGround,
		ForestCaveGround
	};

	public enum Type
	{
		Center,
		South,
		West,
		SouthWest,
		East,
		SouthEast,
		WestEast,
		SouthWestEast,
		North,
		NorthEast,
		NorthWestEast,
		NorthSouthEast,
		NorthSouthWest,
		NorthSouth,
		NorthWest,
		NorthSouthWestEast
	};

    public Vector2 position;
	public bool Up = false;
	public bool Right = false;
	public bool Down = false;
	public bool Left = false;
	public Zone zone;
	public Type type;


#if UNITY_EDITOR
    public void updateTile(){
		SpriteRenderer spriterRenderer = GetComponentInChildren<SpriteRenderer> ();
		GameObject prefab = getPrefabFromTileZoneAndTileName (zone, type);
		createAuthoringInstanceFromPrefab (prefab, this,type);
	}

	GameObject getPrefabFromTileZoneAndTileName(Tile.Zone tileZone, Tile.Type type){
		GameObject tilePrefab = Resources.Load<GameObject>("Prefabs/Environment/Tiles/" 
			+ tileZone.ToString() +
			"/" +
			tileZone.ToString() +"_"+ type) as GameObject;
		return tilePrefab;
	}


    GameObject createAuthoringInstanceFromPrefab(GameObject prefab, Tile tile, Tile.Type type){
		GameObject instance = PrefabUtility.InstantiatePrefab(prefab,SceneManager.GetActiveScene()) as GameObject;
		Undo.RegisterCreatedObjectUndo (instance, "Create instance");
		instance.transform.position = tile.transform.position;
		instance.transform.parent = tile.transform.parent;
		Tile instanceTile = instance.GetComponent<Tile> ();
		instanceTile.position = tile.position;
		instanceTile.type = type;
		instanceTile.zone = zone;
		if (tile.transform.parent.GetComponent<Screen> () == null)
			instance.GetComponent<BoxCollider2D> ().isTrigger = true;
		Undo.DestroyObjectImmediate (tile.gameObject);
		return instance;
	}
#endif
}
