using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WeakWalker : Enemy {

	private float _maxVelocity = 300;

	// Use this for initialization
	void Start () {
		base.Start();
	}

	// Update is called once per frame
	void Update () {

			Vector2 playerDir = m_Player.transform.position - transform.position;
			if (playerDir.x > 0) {
				playerDir.x = 1;
			} else {
				playerDir.x = -1;
			}

		if (m_RigidBody.velocity.x < _maxVelocity) {
			m_RigidBody.AddForce (new Vector2(playerDir.x * 300,0),ForceMode2D.Force);
		}

	}
}
