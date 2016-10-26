using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class Boar : Enemy {
    Animator _anim;
    int shotHash = Animator.StringToHash("shot");

    public float speed;
    public GameObject pointA;
    public GameObject pointB;
    
    private Vector3 A;
    private Vector3 B;
    private Vector3 target;
    private bool isFacingRight;
    private int _StopCount = 0;

    // Use this for initialization
    public override void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        A = pointA.transform.position;
        B = pointB.transform.position;
        target = A;
        transform.localScale = (new Vector3(1, 1, 1));
		base.Start();
    }

    // Update is called once per frame
	public override void Update()
    {
		if (!m_Dead && !m_BeingHit && !m_Stopped && !m_Stunned) {

            Vector3 dir = target - transform.position;

            if (dir.magnitude < 10) // close enough from target
                changeDirection();

            dir = target - transform.position;
            dir.Normalize();

            m_RigidBody.velocity = (dir * speed);

            /*transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);

			if (transform.position == Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime))
				changeDirection ();*/
		}

		base.Update ();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            m_Stopped = true;
            ++_StopCount;
            _anim.SetBool("HitPlayer", true);
            Invoke("ResetStoppedCoolDown", 1);

            m_RigidBody.velocity = new Vector2(0, m_RigidBody.velocity.y);

        }
        base.OnTriggerEnter2D(other);
    }

    void changeDirection() {
        if (target == A)
           {
              target = B;
              transform.localScale = (new Vector3(-1,1,1));
           }
        else
            {
                target = A;
                transform.localScale = (new Vector3(1, 1, 1));
            }
    }


    public override void GetCountered()
    {
        m_Stunned = true;
    }

    public override void ResetStoppedCoolDown()
    {
        --_StopCount;
        if(_StopCount <= 0)
        {
            m_Stopped = false;
            _anim.SetBool("HitPlayer", false);
        }
    }
}
