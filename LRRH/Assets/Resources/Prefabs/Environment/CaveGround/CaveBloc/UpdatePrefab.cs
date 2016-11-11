using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class UpdatePrefab : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var updatedPrefab = new GameObject (transform.name + "_sprite");
		updatedPrefab.transform.SetParent (transform,false);
		updatedPrefab.AddComponent<SpriteRenderer> ();
		updatedPrefab.GetComponent<SpriteRenderer> ().sprite = gameObject.GetComponent<SpriteRenderer> ().sprite;
		var col = gameObject.AddComponent<BoxCollider2D> ();
		col.size = new Vector2 (30,30);
		DestroyImmediate (gameObject.GetComponent<SpriteRenderer>());
		DestroyImmediate (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
