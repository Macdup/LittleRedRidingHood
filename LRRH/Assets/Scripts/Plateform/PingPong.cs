using UnityEngine;
using System.Collections;

public class PingPong : MonoBehaviour {

	public float speed;
	public GameObject pointA;
	public GameObject pointB;

	private Vector3 A;
	private Vector3 B;
	private Vector3 target;

	// Use this for initialization
	public void Start()
	{
		A = pointA.transform.position;
		B = pointB.transform.position;
		target = A;
	}

	// Update is called once per frame
	public void Update()
	{

			transform.position = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);

			if (transform.position == Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime))
				changeDirection ();

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
