using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ExplosionEffect : MonoBehaviour {
	
	void deactivate() {
		gameObject.SetActive (false);
	}

	public void pop(Vector3 position) {
		gameObject.SetActive (true);
		transform.position = position;
	}

	public void OnTriggerEnter2D(Collider2D other) {
		var player = other.GetComponent<Player>();
		if (player != null) {
			player.Bump (transform.position,450);
		}

        
		Enemy enemy = (Enemy)other.gameObject.GetComponent<Enemy> ();
        
        if (enemy != null) {
			enemy.Bump (transform.position, 450);
		}
	}
}
