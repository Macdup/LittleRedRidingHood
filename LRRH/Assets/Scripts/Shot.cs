using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Shot : MonoBehaviour {

    // public member
	public Vector3 MoveVector;
	public float HitDamage;
    public float iStaminaLossPerHit;
    [HideInInspector]
    public GameObject Source;
    [HideInInspector]
    public GameObject Target;

    // protected member
    //protected bool m_Dead = false;

    // private member
    protected Animator m_Anim;
    protected int m_collideHash = Animator.StringToHash("collide");

    // variable

    // Use this for initialization
    virtual public void Start () {
        m_Anim = gameObject.GetComponentInChildren<Animator>();
        flip();
	}

    // Update is called once per frame
    virtual public void Update () {
        move();
	}

	virtual public void OnTriggerEnter2D(Collider2D other) {
		if (Source == null || other.gameObject == Source)
			return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Walls")) {
            if (this.enabled)
                m_Anim.SetTrigger(m_collideHash);
            this.enabled = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Player player = other.gameObject.GetComponent<Player> ();
            if (player != null && Target == player.gameObject)
            {
                if (player._isInCounterTime)
                {
                    MoveVector = Source.transform.position - player.transform.position;
                    MoveVector = MoveVector.normalized * 100.0f;
                    Target = Source;
                    Source = player.gameObject;
                }
                else {
                    player.Hit(HitDamage, iStaminaLossPerHit);
                    if (this.enabled)
                        m_Anim.SetTrigger(m_collideHash);
                    this.enabled = false;
                }
            }

            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null && Target == enemy.gameObject)
            {
                if (enemy._isInCounterTime)
                {
                    MoveVector = Source.transform.position - Target.transform.position;
                    MoveVector = MoveVector.normalized * 100.0f;
                }
                else
                {
                    enemy.Hit(HitDamage);
                    if (this.enabled)
                        m_Anim.SetTrigger(m_collideHash);
                    this.enabled = false;
                }
            }
              
		}
	}

    virtual public void move() {
            if (this.enabled == true)
                transform.Translate(MoveVector * Time.deltaTime,Space.World);
		}

    virtual public void flip() {
        if (MoveVector.x < 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
