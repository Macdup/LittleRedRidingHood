using UnityEngine;
using System.Collections;

public class HiddenWalls : MonoBehaviour {

	public float AlphaWhenShown = 0.3f;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		Player player = other.gameObject.GetComponent<Player> ();
		if (player != null) {
			SpriteRenderer[] sr = this.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer s in sr) {
				Color c = s.color;
				c.a = AlphaWhenShown;
				s.color = c;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		Player player = other.gameObject.GetComponent<Player> ();
		if (player != null) {
			SpriteRenderer[] sr = this.GetComponentsInChildren<SpriteRenderer> ();
			foreach (SpriteRenderer s in sr) {
				Color c = s.color;
				c.a = 1.0f;
				s.color = c;
			}
		}
	}
}
