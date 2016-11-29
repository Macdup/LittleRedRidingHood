using UnityEngine;
using System.Collections;

public class MobilePlatform : MonoBehaviour {

	public float speed;
	public GameObject pointA;
	public GameObject pointB;

	private Vector3 A;
	private Vector3 B;
	private Vector3 target;
	private Rigidbody2D rigidbody;
	private bool activated;
	private bool arrived;



	// Use this for initialization
	public void Start()
	{
		A = pointA.transform.position;
		B = pointB.transform.position;
		target = B;
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.position = A;
		activated = false;
		arrived = true;
		//StartCoroutine ("goTo",target);
	}

	void OnTriggerEnter2D(Collider2D other){
		activated = true;
	}

	void OnTriggerExit2D(Collider2D other){
		activated = false;
	}


	// Update is called once per frame
	void FixedUpdate()
	{
		Vector3 newPos = Vector3.MoveTowards (transform.position, target, speed * Time.deltaTime);
		if (newPos == target) {
			activated = false;
			if (target == A)
				target = B;
			else
				target = A;
		} else {
			rigidbody.MovePosition (newPos);
		}
	}


	IEnumerator goTo(Vector3 pos){
		activated = true;
		Vector3 newPos = Vector3.MoveTowards (transform.position, pos, speed * Time.deltaTime);
		while (newPos != target) {
			rigidbody.MovePosition (newPos);
			newPos = Vector3.MoveTowards (transform.position, pos, speed * Time.deltaTime);
			yield return new WaitForFixedUpdate();
		}
		activated = false;
	}



}
