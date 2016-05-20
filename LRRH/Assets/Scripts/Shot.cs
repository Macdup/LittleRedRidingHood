using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

    // public member
	public Vector3 MoveVector;
	public float HitDamage;
    [HideInInspector]
    public GameObject Source;

    // protected member
    //protected bool m_Dead = false;

    // private member
    private Animator m_Anim;
    private int m_collideHash = Animator.StringToHash("collide");

    // variable

	// Use this for initialization
	void Start () {
        m_Anim = gameObject.GetComponentInChildren<Animator>();
        flip();
	}
	
	// Update is called once per frame
	void Update () {
        move();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (Source == null || other.gameObject == Source)
			return;
		
		if (other.gameObject.layer == LayerMask.NameToLayer ("Player")
		   || other.gameObject.layer == LayerMask.NameToLayer ("Walls")) {
			
            if (this.enabled) {
				m_Anim.SetTrigger (m_collideHash);
			}
            Player player = other.gameObject.GetComponent<Player> ();
            //Debug.Log(player);
            if (player != null)
            {
                player.Hit(HitDamage);
            }
		}
	}

    virtual public void move() {
            if (this.enabled == true)
                transform.Translate(MoveVector * Time.deltaTime,Space.World);
		}

    void flip() {
        if (MoveVector.x < 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
