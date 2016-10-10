using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar : MonoBehaviour {

	public Screen[] ActionZone;
	//La liste de l'ensemble des tiles du niveau
	public Tile[] OpenList;
	//La liste des tiles qui ne sont plus à visiter
	public Tile[] ClosedList;

	// Use this for initialization
	void Start () {
		getDiscreteTiles (ActionZone [0]);
	}

	// Update is called once per frame
	void Update () {

	}

	List<Tile> getDiscreteTiles(Screen screen){
		var raw = -10;
		var column = 17;
		List<Tile> tilesList = new List<Tile>();
		for (var i = 0; i >= raw; i--) {
			for (var j = 0; j <= column; j++) {
				var tile = new Tile ();
				tile.position = new Vector2 (screen.minBound.x + j,screen.minBound.y + i);
				tilesList.Add (tile);
			}
		}
		return tilesList;
	}
}
