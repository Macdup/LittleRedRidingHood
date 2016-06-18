using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class MagicShot : Shot {

    public float MagicShotDuration = 3.0f;
    public float MagicShotLongAttackDamageValue;
    public float BumpForce = 10.0f;

    private float _elapseTimeFromStart = 0.0f;
    private bool _triggered = false;

	
	// Update is called once per frame
	void FixedUpdate () {
        _elapseTimeFromStart += Time.fixedDeltaTime;
        if(_elapseTimeFromStart>MagicShotDuration)
        {
            m_Anim.SetBool("ShotEnd", true);
        }
    }

    public override void move()
    {
        if (!_triggered)
            transform.Translate(MoveVector * Time.deltaTime, Space.World);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (Source == null || other.gameObject == Source || _triggered)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            m_Anim.SetTrigger(m_collideHash);
            _triggered = true;
        }
        else
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                //    m_Anim.SetTrigger(m_collideHash);
                //    enemy.Hit(HitDamage);
                //    _triggered = true;
                //}
                

                /*if (Source.GetComponent<Player>().IsAttackLongCasted())
                    enemy.Hit(MagicShotLongAttackDamageValue);
                else*/
                    enemy.Hit(HitDamage);


                if (enemy.IsBumpable)
                {
                    enemy.Bump(this.transform.position, BumpForce);
                }
                m_Anim.SetTrigger(m_collideHash);
                _triggered = true;
            }

            BushScript otherBushScript = other.gameObject.GetComponent<BushScript>();
            if (otherBushScript != null)
            {
                otherBushScript.hit();
                m_Anim.SetTrigger(m_collideHash);
                _triggered = true;
            }
        }
    }

    public void ShotEndFinish()
    {
        Object.Destroy(this.gameObject);
    }
}
