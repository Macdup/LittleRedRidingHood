using UnityEngine;
using System.Collections;

public class playerAnim : MonoBehaviour {

    private Player playerScript; 

	// Use this for initialization
	void Start () {
        playerScript = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void destroy(){
		GameObject.Destroy(transform.parent.gameObject);
	}

    public void idleEnd() {
        playerScript.setIdle(false);
    }
}
