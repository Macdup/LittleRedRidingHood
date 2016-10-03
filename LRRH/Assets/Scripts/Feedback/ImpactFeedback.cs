using UnityEngine;
using System.Collections;

public class ImpactFeedback : MonoBehaviour {

	// Use this for initialization
	void Awake () {

	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void deactivate() {
		gameObject.SetActive (false);
	}

	public void pop(Vector3 position) {
		gameObject.SetActive (true);
		transform.position = position;
		Invoke ("deactivate", 0.5f);
	}
}
