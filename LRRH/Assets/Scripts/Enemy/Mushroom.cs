using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Mushroom : Enemy {

    public GameObject ShootPrefab;
    public float ShootSpeed = 100.0f;
	public float ShootCoolDown = 3.0f;

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
		if (!m_Dead) {
            GameObject shotInstance = (GameObject)Instantiate(ShootPrefab);
			Shot shot = shotInstance.GetComponent<Shot> ();
            shot.Source = this.gameObject;
            shot.HitDamage = DamagePerHit;
            shot.Target = m_Player.gameObject;
            shot.iStaminaLossPerHit = staminaLossPerHit;
			shotInstance.GetComponent<Shot> ().MoveVector = new Vector2 (ShootSpeed * transform.localScale.x * -1, 0);
			shotInstance.transform.position = new Vector3 (this.transform.position.x + (transform.localScale.x * (-20)), this.transform.position.y + 10, this.transform.position.z);
			Invoke ("startShot", ShootCoolDown);
		}
    }

}
