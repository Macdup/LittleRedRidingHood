using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Mushroom : Enemy {

    public GameObject ShootPrefab;
    public float ShootSpeed = 100.0f;
	public float ShootCoolDown = 3.0f;

    Animator _anim;
    int shotHash = Animator.StringToHash("shot");
    private BoxCollider2D _MushroomZone;

    // Use this for initialization
    public override void Start () {
        _anim = GetComponentInChildren<Animator>();
        _MushroomZone = transform.GetChild(1).GetComponent<BoxCollider2D>();
		base.Start ();
	}

    public void FixedUpdate()
    {
        bool playerInZone = _MushroomZone.OverlapPoint(new Vector2(m_Player.transform.position.x, m_Player.transform.position.y));
        if(playerInZone && !_anim.GetBool("PlayerInZone"))
        {
            _anim.SetBool("PlayerInZone", playerInZone);
            startShot();
        }
        else
            _anim.SetBool("PlayerInZone", playerInZone);
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
