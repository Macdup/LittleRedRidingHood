using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class FlyingSnake : Enemy {

	public Vector3 startVelocity;
	Vector3 speedVector;

	// Use this for initialization
	public override void Start () {
		speedVector = startVelocity * 10000;
		base.Start();
	}

	// Update is called once per frame
	void FixedUpdate () {
		m_RigidBody.velocity =  speedVector * Time.fixedDeltaTime;

		var test = speedVector;
		Quaternion rotation = Quaternion.LookRotation (speedVector,Vector3.up);
		transform.rotation = rotation;
		transform.Rotate (Vector3.up, -90);
		//transform.rotation = Quaternion.AngleAxis (90, Vector3.up);
	}

	void OnCollisionEnter2D(Collision2D other) {
		Tile tile = other.gameObject.GetComponent<Tile> ();

		if (tile != null) {
			speedVector = Vector3.Reflect (speedVector, other.contacts [0].normal);
			Debug.Log (other.contacts [0].normal);
		}
	}
}
