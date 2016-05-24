using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WeaponScript : MonoBehaviour {

	public float DamagaValue = 100.0f;
	public float BumpForce = 10.0f;
    public float StaminaConsomation = 10.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		//Debug.Log(other.name);
		Enemy enemy = other.gameObject.GetComponent<Enemy> ();
		if (enemy != null) {
			enemy.Hit (DamagaValue);

			if (enemy.IsBumpable) {
				enemy.Bump (this.transform.position, BumpForce);
			}
		}

        BushScript otherBushScript = other.gameObject.GetComponent<BushScript>();
        if (otherBushScript != null)
        {
            otherBushScript.hit();
        }
	}
}
