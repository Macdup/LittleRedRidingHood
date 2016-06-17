using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class WeaponScript : MonoBehaviour {

	public float DamageValue = 5.0f;
    public float LongAttackDamageValue = 15.0f;
    public float BumpForce = 10.0f;
    public float StaminaConsomation = 10.0f;
    public GameObject HitPrefab;

    private Player m_Player;

	// Use this for initialization
	void Start () {
        m_Player = GetComponentInParent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {

		Enemy enemy = other.gameObject.GetComponent<Enemy> ();
		
        if (enemy != null && enemy.m_BeingHit == false) {

            Vector2 dir = other.bounds.center - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 200f, LayerMask.GetMask("Enemy"));

            if (m_Player.IsAttackLongCasted())
                enemy.Hit(LongAttackDamageValue);
            else
                enemy.Hit(DamageValue);

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
