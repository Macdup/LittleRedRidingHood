using UnityEngine;
using System.Collections;
using AssemblyCSharp;

enum stateOfMind { normal,tired,angry };

public class FuriousDog : Enemy {

    // Member
    private BoxCollider2D m_BottomBox;

    // variable
    private float _energy = 5;
    private float _maxEnergy = 100;
    private bool _PlayerInSight;
    private stateOfMind _stateOfMind = stateOfMind.normal;
    private bool m_BottomTouched;
    private LayerMask _wallsMask;

    // should be in some PlayerAnim script !!!
    int _jumpHash = Animator.StringToHash("jump");
    int _tiredHash = Animator.StringToHash("tired");
    int _velocityYHash = Animator.StringToHash("velocityY");

    public override void Start()
    {
        base.Start();
        m_BottomBox = GetComponent<BoxCollider2D>();
        _wallsMask = LayerMask.GetMask("Walls");
    }

    // Use this for initialization
    public override void Update () {
        detectPlayer();
    }

    void FixedUpdate()
    {
        // Evalute states
        m_BottomTouched = m_BottomBox.IsTouchingLayers(_wallsMask);

        if (m_BottomTouched)
            m_Animator.SetBool(_jumpHash, false);
        

        if (_PlayerInSight == true && _energy > 0 && m_BottomTouched == true)
        {
            attack();
            m_Animator.SetBool(_jumpHash, true);
        }

        if(_energy <= 0)
            m_Animator.SetBool(_tiredHash, true);

        m_Animator.SetFloat(_velocityYHash, m_RigidBody.velocity.y);
    }

    void detectPlayer()
    {
        Vector3 playerDir = m_Player.transform.position - transform.position;
        playerDir.y += 50;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, playerDir, 150);
        Debug.DrawRay(transform.position,playerDir.normalized * 150,Color.white);

        if (hits.Length > 0) {
            for (var i = 0; i < hits.Length; i++) {
                var player = hits[i].transform.GetComponent<Player>();
                if (player != null) {
                    _PlayerInSight = true;
                    flip();
                }
            }
        }

    }

    void attack() {
        Vector2 playerDir = m_Player.transform.position - transform.position;
		if (playerDir.x > 0) 
			playerDir.x = 1;
		else 
			playerDir.x = -1;
		playerDir.y = Random.Range (0.5f,1.5f);
		m_RigidBody.AddForce(playerDir.normalized * 80, ForceMode2D.Impulse);
        _energy = _energy -  1/5f;
        if (_energy < 0)
            StartCoroutine(FillEnergy());
    }

    void flip() {
        Vector2 playerDir = m_Player.transform.position - transform.position;
        if (playerDir.x > 0)
            transform.localScale = new Vector3(-1,1,1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator FillEnergy() {
        yield return new WaitForSeconds(2.0f);
        _energy = 5;
        m_Animator.SetBool(_tiredHash, false);
    }


  
}
