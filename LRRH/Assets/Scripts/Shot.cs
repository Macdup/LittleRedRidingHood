using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

    // public member
	public Vector3 MoveVector;
	public float HitDamage;
    public float iStaminaLossPerHit;
    [HideInInspector]
    public GameObject Source;

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
		
		if (other.gameObject.layer == LayerMask.NameToLayer ("Player")
		   || other.gameObject.layer == LayerMask.NameToLayer ("Walls")) {
			
            if (this.enabled) {
				m_Anim.SetTrigger (m_collideHash);
			}
            Player player = other.gameObject.GetComponent<Player> ();
            if (player != null)
            {
                player.Hit(HitDamage,iStaminaLossPerHit);
            }
            this.enabled = false;
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
