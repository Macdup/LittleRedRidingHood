using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class IAWeaponScript : MonoBehaviour {

	public float DamageValue = 5.0f;
    public float LongAttackDamageValue = 15.0f;
    public float BumpForce = 10.0f;
    public float StaminaConsomation = 10.0f;
    public GameObject HitPrefab;

    private IATest m_IATest;

	// Use this for initialization
	void Start () {
        m_IATest = GetComponentInParent<IATest>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {

		Player player = other.gameObject.GetComponent<Player> ();

        if (player != null && player.m_BeingHit == false)
        {

            Vector2 dir = other.bounds.center - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 200f, LayerMask.GetMask("Player"));

            if (m_IATest.IsAttackLongCasted())
                player.Hit(LongAttackDamageValue, StaminaConsomation);
            else
                player.Hit(DamageValue, StaminaConsomation);

            Instantiate(HitPrefab, hit.point, Quaternion.identity);

            /*if (player.IsBumpable)
            {
                player.Bump(this.transform.position, BumpForce);
			}*/
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
