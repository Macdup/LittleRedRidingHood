using UnityEngine;
using System.Collections;

public class PingPong : MonoBehaviour {

	public float speed;
	public GameObject pointA;
	public GameObject pointB;

	private Vector3 A;
	private Vector3 B;
	private Vector3 target;
    private Rigidbody2D rigidbody;

	// Use this for initialization
	public void Start()
	{
		A = pointA.transform.position;
		B = pointB.transform.position;
		target = A;
        rigidbody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void FixedUpdate()
	{

			Vector3 newPos = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
            rigidbody.MovePosition(newPos);


            if (transform.position == newPos)
				changeDirection ();

	}

	void changeDirection() {
		if (target == A)
		{
			target = B;
			//transform.localScale = (new Vector3(-1,1,1));
		}
		else
		{
			target = A;
			//transform.localScale = (new Vector3(1, 1, 1));
		}
	}

}
