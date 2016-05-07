#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

[ExecuteInEditMode]
public class TileManager : MonoBehaviour {

	//public member
	public Texture2D Map;
	public float TileSize = 30;
	public bool DoGenerate = false;
	public bool DoClean = false;
	public GameObject[] TilesPrefab;

	// private member


	// variable
	public Vector2 _leftTopPixel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (DoGenerate) {
			if(TilesPrefab.Length >0)
				LoadMap (Map);
			DoGenerate = false;
		}
		if (DoClean) {
			CleanTransform (transform);
			DoClean = false;
		}
	}

	void CleanTransform(Transform iTransform) {
		for(int i=0 ; i<iTransform.childCount ; ++i) {
			CleanTransform (iTransform.GetChild(i));
			DestroyImmediate (iTransform.GetChild(i).gameObject);
			--i;
		}
	}


	void LoadMap(Texture2D iMap)
	{
		// clean 
		CleanTransform (transform);
		_leftTopPixel = new Vector2 (1, 1);
		this.transform.position = new Vector3 (0,0, this.transform.position.z);

		// compute each pixel
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
					tile.transform.parent = this.transform;
					tile.layer = LayerMask.NameToLayer("Walls");


					// compute left top pixel
					if (i <= _leftTopPixel.x) {
						_leftTopPixel.x = i;
						if(j > _leftTopPixel.y)
							_leftTopPixel.y = j;
					}
				}
			}
		} 

		this.transform.position = new Vector3 (-_leftTopPixel.x*TileSize, (-_leftTopPixel.y -1)*TileSize, this.transform.position.z);
	}
}


#endif