using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlayerColliderGenerator : MonoBehaviour {

	public bool GenerateCollider = false;

	public float PlayerHeight = 50.0f;
	public float PlayerWidth = 26.0f;
	public Vector2 PlayerOffset = new Vector2(-1.0f, 0.0f);
	public float CircleRadius = 4.0f;

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
			main_box.size = new Vector2 (PlayerWidth-2.0f*CircleRadius, PlayerHeight-CircleRadius);
			main_box.offset = PlayerOffset + new Vector2 (0.0f, (CircleRadius+PlayerHeight)/2.0f);

			var bottom_left = this.gameObject.AddComponent<CircleCollider2D> ();
			bottom_left.radius = CircleRadius;
			bottom_left.offset = PlayerOffset + new Vector2 (CircleRadius-PlayerWidth/2.0f, CircleRadius);

			var bottom_right = this.gameObject.AddComponent<CircleCollider2D> ();
			bottom_right.radius = CircleRadius;
			bottom_right.offset = PlayerOffset + new Vector2 (PlayerWidth/2.0f-CircleRadius, CircleRadius);

			var bottom_box = this.gameObject.AddComponent<BoxCollider2D> ();
			bottom_box.size = new Vector2 (PlayerWidth-2.0f*CircleRadius, CircleRadius);
			bottom_box.offset = PlayerOffset + new Vector2 (0, CircleRadius/2.0f);

			var left_box = this.gameObject.AddComponent<BoxCollider2D> ();
			left_box.size = new Vector2 (CircleRadius, PlayerHeight-CircleRadius);
			left_box.offset = PlayerOffset + new Vector2 ((CircleRadius-PlayerWidth)/2.0f, (CircleRadius+PlayerHeight)/2.0f);

			var right_box = this.gameObject.AddComponent<BoxCollider2D> ();
			right_box.size = new Vector2 (CircleRadius, PlayerHeight-CircleRadius);
			right_box.offset = PlayerOffset + new Vector2 ((PlayerWidth-CircleRadius)/2.0f, (CircleRadius+PlayerHeight)/2.0f);


			GenerateCollider = false;
		}
	}
}
