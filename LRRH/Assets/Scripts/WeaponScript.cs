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
    private CameraScript m_Camera;
	private ImpactFeedbackManager m_ImpactFeedbackManager;

    // Use this for initialization
    void Start () {
        m_Player = GetComponentInParent<Player>();
        m_Camera = Camera.main.GetComponent<CameraScript>();
		m_ImpactFeedbackManager = GameObject.Find("ImpactFeedbackManager").GetComponent<ImpactFeedbackManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy> ();
		
        if (enemy != null)
        {

            Vector2 dir = other.bounds.center - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 200f, LayerMask.GetMask("Enemy"));
            m_Camera.setShake(0.5f,5);

            if(enemy.m_BeingHit == false)
            {
                m_Player.DoHitEnemy(enemy);

			    ImpactFeedback impact = m_ImpactFeedbackManager.getUsableImpact();
                if(impact!=null)
			        impact.pop (hit.point);
            }

            dir.Normalize();
            enemy.GetComponent<Rigidbody2D>().AddForceAtPosition(BumpForce*dir, hit.point);
			
		}

        BushScript otherBushScript = other.gameObject.GetComponent<BushScript>();
        if (otherBushScript != null)
        {
            Vector2 dir = other.bounds.center - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 200f, LayerMask.GetMask("Objects"));
			ImpactFeedback impact = m_ImpactFeedbackManager.getUsableImpact();
			impact.pop (hit.point);

            otherBushScript.hit();
        }
	}

}
