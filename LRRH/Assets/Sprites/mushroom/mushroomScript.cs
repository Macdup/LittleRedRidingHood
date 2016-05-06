using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class mushroomScript : Enemy {

    public GameObject ShootPrefab;
    public float ShootSpeed = 10.0f;
    public float ShootCoolDown = 30f;

    Animator _anim;
    int shotHash = Animator.StringToHash("shot");

	// Use this for initialization
	public override void Start () {
        _anim = GetComponentInChildren<Animator>();
        Invoke("startShot", ShootCoolDown);

		base.Start ();
	}

    void startShot() {
        _anim.SetTrigger(shotHash);
    }

    public void shot() {
        GameObject shotInstance = (GameObject)Instantiate(ShootPrefab);
		shotInstance.GetComponent<Shot> ().Source = this.gameObject;
        shotInstance.GetComponent<Shot>().moveVector = new Vector2(ShootSpeed * transform.localScale.x * -1, 0);
        shotInstance.transform.position = new Vector3(this.transform.position.x + (transform.localScale.x * (-20)), this.transform.position.y + 10, this.transform.position.z);
        Invoke("startShot", ShootCoolDown);
    }

}
