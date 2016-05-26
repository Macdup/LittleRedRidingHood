using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WeaponScript : MonoBehaviour {

	public float DamagaValue = 100.0f;
	public float BumpForce = 10.0f;
    public float StaminaConsomation = 10.0f;
    public GameObject HitPrefab;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {

		Enemy enemy = other.gameObject.GetComponent<Enemy> ();
		
        if (enemy != null) {

            Vector2 dir = other.bounds.center - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 200f, LayerMask.GetMask("Enemy"));
            enemy.Hit(DamagaValue);

            Instantiate(HitPrefab, hit.point, Quaternion.identity);

			if (enemy.IsBumpable) {
				enemy.Bump (this.transform.position, BumpForce);
			}
		}

        BushScript otherBushScript = other.gameObject.GetComponent<BushScript>();
        if (otherBushScript != null)
        {
            Vector2 dir = other.bounds.center - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 200f, LayerMask.GetMask("Objects"));
            Instantiate(HitPrefab, hit.point, Quaternion.identity);

            otherBushScript.hit();
        }
	}
}
