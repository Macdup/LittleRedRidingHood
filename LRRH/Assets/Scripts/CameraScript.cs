using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject ObjectToFollow;
	public float yOffset = 0;
    public float xOffset = 0;
    public float DistanceMax = 170;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ObjectToFollow != null){
			Vector3 pos = this.transform.position;
            pos.x = ObjectToFollow.transform.position.x + (xOffset * ObjectToFollow.transform.localScale.x);
			pos.y = ObjectToFollow.transform.position.y + yOffset;
            this.transform.position = Vector3.Slerp(this.transform.position,pos,0.03f);

            // make sure the object is always visible

            Vector2 dir = new Vector2(ObjectToFollow.transform.position.x, ObjectToFollow.transform.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
            float dist = dir.magnitude;
            if (dir.magnitude > DistanceMax)
            {
                dir.Normalize();
                Vector2 adjust = dir * (dist - DistanceMax);
                pos = this.transform.position;
                pos.x += adjust.x;
                pos.y += adjust.y;
                this.transform.position = pos;
            }

        }

	}
}
