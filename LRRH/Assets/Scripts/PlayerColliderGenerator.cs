using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlayerColliderGenerator : MonoBehaviour {

	public bool GenerateCollider = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GenerateCollider) {
			// first thing first, clean collider
			Collider2D[] colliders = this.gameObject.GetComponents<Collider2D>();
			foreach(Collider2D c in colliders){
				DestroyImmediate(c);
			}


			var main_box = this.gameObject.AddComponent<BoxCollider2D> ();
			main_box.size = new Vector2 (110, 240);
			main_box.offset = new Vector2 (0, 140);

			var bottom_left = this.gameObject.AddComponent<CircleCollider2D> ();
			bottom_left.radius = 20;
			bottom_left.offset = new Vector2 (-55, 20);

			var bottom_right = this.gameObject.AddComponent<CircleCollider2D> ();
			bottom_right.radius = 20;
			bottom_right.offset = new Vector2 (55, 20);

			var bottom_box = this.gameObject.AddComponent<BoxCollider2D> ();
			bottom_box.size = new Vector2 (110, 20);
			bottom_box.offset = new Vector2 (0, 10);

			var left_box = this.gameObject.AddComponent<BoxCollider2D> ();
			left_box.size = new Vector2 (20, 240);
			left_box.offset = new Vector2 (-65, 140);

			var right_box = this.gameObject.AddComponent<BoxCollider2D> ();
			right_box.size = new Vector2 (20, 240);
			right_box.offset = new Vector2 (65, 140);


			GenerateCollider = false;
		}
	}
}
