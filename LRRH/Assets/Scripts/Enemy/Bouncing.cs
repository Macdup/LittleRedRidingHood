using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Bouncing : Enemy {

	private float _bounceTimer = 1;

	// Use this for initialization
	void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		_bounceTimer -= 1 * Time.deltaTime;

		if (_bounceTimer < 0) {
			Vector2 playerDir = m_Player.transform.position - transform.position;
			if (playerDir.x > 0) {
				playerDir.x = 1;
			} else {
				playerDir.x = -1;
			}
			playerDir.x = playerDir.x * 40;
			playerDir.y = 80;
			m_RigidBody.AddForce(playerDir, ForceMode2D.Impulse);
			_bounceTimer = 1;
		}
	}
}
