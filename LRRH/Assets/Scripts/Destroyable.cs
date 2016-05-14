using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void destroy() {
		GameObject.Destroy(transform.parent.gameObject);
	}
}
