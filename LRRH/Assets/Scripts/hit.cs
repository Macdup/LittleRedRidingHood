using UnityEngine;
using System.Collections;

public class hit : MonoBehaviour {

	public float DamagePerHit;
	public bool m_Stunned;


	public void OnTriggerEnter2D(Collider2D other) {

		Player player = other.gameObject.GetComponent<Player> ();

		if (player != null) {
			player.Hit(this, DamagePerHit,0);

			//Bump player
			if (!m_Stunned) {
				player.Bump(this.transform.position, 0);
			}
		}

	}
}
