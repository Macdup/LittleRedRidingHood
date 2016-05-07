using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WeaponScript : MonoBehaviour {

	public float DamagaValue = 100;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		Enemy otherEnemy = other.gameObject.GetComponent<Enemy> ();
		if (otherEnemy != null) {
			otherEnemy.Hit (DamagaValue);
		}

        BushScript otherBushScript = other.gameObject.GetComponent<BushScript>();
        if (otherBushScript != null)
        {
            otherBushScript.BushAnim.explode();
        }
	}
}
