using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject ObjectToFollow;
	public float yOffset = 0;
    public float xOffset = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(ObjectToFollow != null){
			Vector3 pos = this.transform.position;
            pos.x = ObjectToFollow.transform.position.x ;
			pos.y = ObjectToFollow.transform.position.y ;
			this.transform.position = Vector3.Lerp(this.transform.position, pos, 0.1f);
		}

	}
}
