using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Spiker : Enemy
{
    public float TriggerDistance = 100.0f;

    private BoxCollider2D _TriggerZone;
    private GameObject _Player;
    private Animator _Animator;

    private bool _isTriggered = false;

	// Use this for initialization
	void Start () {
        //_TriggerZone = GetComponents<BoxCollider2D>()[1];
        _Player = GameObject.Find("Player");
        _Animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if(m_Dead)
        {
            _Animator.SetBool("Dead", true);
            return;
        }
        
        bool isInZone = Vector2.Distance(new Vector2(_Player.transform.position.x, _Player.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y)) < TriggerDistance;
        if(isInZone)
        {
            if(!_isTriggered)
            {
                _Animator.SetBool("Triggered", true);
                _isTriggered = true;
            }
        }
        else
        {
            _Animator.SetBool("Triggered", false);
            _isTriggered = false;
        }
    }
		

    public override void Death()
    {
        m_Dead = true;
       
        if (m_Dropable != null)
            m_Dropable.drop();
    }

    public void EndDead()
    {
        Object.Destroy(this.gameObject);
    }
}
