using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class ExplosionEffect : MonoBehaviour {

	public float damage;

	// member
	private CameraScript m_Camera;
	private ImpactFeedbackManager m_ImpactFeedbackManager;

	void Awake() {
		m_Camera = Camera.main.GetComponent<CameraScript>();
		m_ImpactFeedbackManager = GameObject.Find("ImpactFeedbackManager").GetComponent<ImpactFeedbackManager>();
	}

	void deactivate() {
		gameObject.SetActive (false);
	}

	public void pop(Vector3 position) {
		gameObject.SetActive (true);
		transform.position = position;
		m_Camera.setShake(1.0f,10);
	}

	public void OnTriggerEnter2D(Collider2D other) {
		var player = other.GetComponent<Player>(); 
		if (player != null) {
			player.Hit (damage,damage);
			player.Bump (transform.position,400);
		}

        
		Enemy enemy = (Enemy)other.gameObject.GetComponent<Enemy> ();
        
        if (enemy != null) {
			enemy.Hit (damage);
			enemy.Bump (transform.position, 400);
		}

		ImpactFeedback impact = m_ImpactFeedbackManager.getUsableImpact();
		impact.pop (other.transform.position);
			
	}
}
