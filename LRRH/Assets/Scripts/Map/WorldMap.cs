using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldMap : MonoBehaviour {

    public Vector2 mapSize = new Vector2(50,50);
    public Vector2 tileSize = new Vector2(30,30);
    public Vector2 gridSize = new Vector2();
    public Vector2 ScreenSize = new Vector2();
    public GameObject tiles;
    public Brush BrushFeedback;
	public List<GameObject> Screens = new List<GameObject>();

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnDrawGizmosSelected() {
        var pos = transform.position;
		pos.x -= mapSize.x * tileSize.x;
		pos.y += mapSize.y * tileSize.y;

            Gizmos.color = Color.gray;
            var row = 0;
            var maxColumns = mapSize.x * 2;
            var total = mapSize.x * mapSize.y * 4;
            var tile = new Vector3(tileSize.x, tileSize.y,0);
            var offset = new Vector2(tile.x/2, tile.y/2);
            
            // On dessine les tiles
            for (var i = 0; i < total; i++) {
                var column = i % maxColumns;
                
                var newX = (column * tile.x) + offset.x + pos.x;
                var newY = -(row * tile.y) - offset.y + pos.y;

                //Gizmos.DrawWireCube(new Vector2(newX, newY), tile);
                if (column == maxColumns - 1)
                {
                    row++;
                }
            }

            // On dessine les limites de la grille
            gridSize.x = mapSize.x * tileSize.x;
			gridSize.y = mapSize.y * tileSize.y;
            Gizmos.color = Color.white;
            var centerX = pos.x + (gridSize.x / 2);
            var centerY = pos.y -(gridSize.y / 2);
            Gizmos.DrawWireCube(new Vector2(centerX, centerY), gridSize);
			Gizmos.DrawWireCube(new Vector2(-centerX, centerY), gridSize);
			Gizmos.DrawWireCube(new Vector2(-centerX, -centerY), gridSize);
			Gizmos.DrawWireCube(new Vector2(centerX, -centerY), gridSize);    

            // On crée le feedback de création de zone à la sélection de la map.
            if (BrushFeedback == null) {
                var go = new GameObject("Brush");
                go.transform.SetParent(transform);
                BrushFeedback = go.AddComponent<Brush>();
            }
           
        }

}
