#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

[ExecuteInEditMode]
public class TileManager : MonoBehaviour {

	//public TextAsset Map;
	public Texture2D Map;
	public Texture2D GroundTileset;
	//_public GameObject TileBlock;
	public double TileSize = 30;
	public bool DoGenerate = false;



	public GameObject[] TilesPrefab;
	private List<GameObject> _generatedTiles = new List<GameObject>();	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (DoGenerate) {
			//string spritePath = AssetDatabase.GetAssetPath(GroundTileset);
			//TilesPrefab = AssetDatabase.LoadAllAssetsAtPath (spritePath).OfType<Sprite>().ToArray();
			if(TilesPrefab.Length >0)
				LoadMap (Map);
			DoGenerate = false;
		}
	}



	void LoadMap(Texture2D iMap)
	{
		// clean
		foreach (GameObject g in _generatedTiles) {
			DestroyImmediate (g);
		}
		_generatedTiles.Clear();

		//Texture2D tex = new Texture2D(0, 0);
		//tex.LoadImage(iMap.bytes);

		for (int i = 0; i < iMap.width; i++)
		{
			for (int j = 0; j < iMap.height; j++)
			{
				Color pixel = iMap.GetPixel(i,j);

				if (pixel.a != 0)
				{
					if (j - 1 < 0 || j + 1 > iMap.height || i - 1 < 0 || i + 1 > iMap.width) {
						Debug.LogError ("BAD TEXTURE MARGIN");
						continue;
					}

					bool hasPixelAbove = iMap.GetPixel (i, j + 1).a != 0;
					bool hasPixelOnTheLeft = iMap.GetPixel (i - 1, j).a != 0;
					bool hasPixelOnTheRight = iMap.GetPixel (i + 1, j).a != 0;
					bool hasPixelUnder = iMap.GetPixel (i, j - 1).a != 0;

					int tileIndex = 4;

					if (!hasPixelAbove) {
						if (!hasPixelUnder) {
							if (!hasPixelOnTheLeft) {
								if (!hasPixelOnTheRight) {
									// nothing around
								} else {
									// nothing but right
									tileIndex = 9;
								}
							} else {
								if (!hasPixelOnTheRight) {
									// nothing but left
									tileIndex = 11;
								} else {
									// something left and right
									tileIndex = 10;
								}
							}
						} else {
							if (!hasPixelOnTheLeft) {
								if (!hasPixelOnTheRight) {
									// something under
									tileIndex = 12;
								} else {
									// something right and under
									tileIndex = 0;
								}
							} else {
								if (!hasPixelOnTheRight) {
									// something left and under
									tileIndex = 2;
								} else {
									// something left, right and under
									tileIndex = 1;
								}
							}
						}
					} else {
						if (!hasPixelUnder) {
							if (!hasPixelOnTheLeft) {
								if (!hasPixelOnTheRight) {
									// something above
									tileIndex = 14;
								} else {
									// something right and above
									tileIndex = 6;
								}
							} else {
								if (!hasPixelOnTheRight) {
									// something left and above
									tileIndex = 8;
								} else {
									// something left, right and above
									tileIndex = 7;
								}
							}
						} else {
							if (!hasPixelOnTheLeft) {
								if (!hasPixelOnTheRight) {
									// something above and under
									tileIndex = 13;
								} else {
									// something right, above and under
									tileIndex = 3;
								}
							} else {
								if (!hasPixelOnTheRight) {
									// something left, above and under
									tileIndex = 5;
								} else {
									// something left, right , above and under
									tileIndex = 4;
								}
							}
						}
					}
						

					GameObject tile = (GameObject)Instantiate (TilesPrefab[tileIndex], new Vector3 ((float)(i * TileSize), (float)(j * TileSize), this.transform.position.z), new Quaternion ());
					tile.name = "Tile " + i + " " + j;
					//tile.transform.position = new Vector3 ((float)(i * TileSize), (float)(j * TileSize), this.transform.position.z);
					tile.transform.parent = this.transform;
					tile.layer = LayerMask.NameToLayer("Walls");

					SpriteRenderer sr = tile.AddComponent<SpriteRenderer> ();

					//BoxCollider2D bc =tile.AddComponent<BoxCollider2D> ();

					_generatedTiles.Add(tile);

				}
			}
		} 
	}
}


#endif