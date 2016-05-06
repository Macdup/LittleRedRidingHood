using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public GameObject Source;
	public Vector3 moveVector;
	public int hitDamage;

	Animator anim;
	int collideHash = Animator.StringToHash("collide");

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.enabled == true)
		transform.Translate(moveVector * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject == Source)
			return;
        if (this.enabled) {
            anim.SetTrigger(collideHash);
            GameObject collider = other.gameObject;
            if (collider.GetComponentInParent<Player>() != null && other is BoxCollider2D == true)
            {
                collider.GetComponentInParent<Player>().Hit(hitDamage);
                this.enabled = false;
            };
        }
	}

}
