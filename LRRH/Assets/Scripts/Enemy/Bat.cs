using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Bat : Enemy {

	public float speed;
	public float detectionRadius;
	public float zTolerance;
	public bool detected;
	private bool isFirstDetection= true;
	public bool atPlayerLevel;
	private bool isfirstAtPlayerLevel = true;
	private Vector3 dirVector = Vector3.zero;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		float distance = Vector3.Distance (transform.position, m_Player.transform.position);
		if (distance < detectionRadius && isFirstDetection == true) {
			isFirstDetection = false;
			detected = true;
		}

		float zDistance = Mathf.Abs (transform.position.y - m_Player.transform.position.y);
		if (detected == true && zDistance < zTolerance) {
			atPlayerLevel = true;
			detected = false;
		}
			
		if (detected == true) {
			Vector3 playerDirection = m_Player.transform.position - transform.position;
			playerDirection.Normalize ();
			dirVector = playerDirection;
		} else if (atPlayerLevel == true && isfirstAtPlayerLevel == true) {
			isfirstAtPlayerLevel = false;
			Vector3 playerDirection = m_Player.transform.position - transform.position;
			playerDirection.Normalize ();
			playerDirection.y = 0;
			playerDirection.z = 0;
			dirVector = playerDirection;
			speed *= 1.5F;
			atPlayerLevel = false;
		}

		transform.Translate (dirVector * Time.deltaTime * speed);
	}


	public void OnDrawGizmos() {
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}
