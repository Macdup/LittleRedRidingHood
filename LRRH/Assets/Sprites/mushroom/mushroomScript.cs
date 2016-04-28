using UnityEngine;
using System.Collections;

public class mushroomScript : MonoBehaviour {

    public GameObject player;
    public float life;

    public GameObject ShootPrefab;
    public float ShootSpeed = 10.0f;
    public float ShootCoolDown = 30f;

	// Use this for initialization
	void Start () {
        shot();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void hit() { 
        
    }

    void death() { 
        
    }

    void shot() {
        GameObject shotInstance = (GameObject)Instantiate(ShootPrefab);
        shotInstance.GetComponent<Shot>().moveVector =  new Vector2(ShootSpeed * transform.localScale.x, 0);
        shotInstance.transform.position = new Vector3(this.transform.position.x + (transform.localScale.x * 30), this.transform.position.y, this.transform.position.z);
        Invoke("shot", ShootCoolDown);
    }

}
