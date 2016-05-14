using UnityEngine;
using System.Collections;
using AssemblyCSharp;


public class boar : Enemy {
    Animator _anim;
    int shotHash = Animator.StringToHash("shot");

    public float speed;
    public GameObject pointA;
    public GameObject pointB;
    
    private Vector3 A;
    private Vector3 B;
    private Vector3 target;
    private bool isFacingRight;

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
		if (!m_Dead && !m_BeingHit) {
			
			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);

			if (transform.position == Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime))
				changeDirection ();
		}

		base.Update ();
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
		
}
