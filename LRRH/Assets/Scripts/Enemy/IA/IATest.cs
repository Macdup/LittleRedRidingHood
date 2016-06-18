using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class IATest : Enemy
{
    // public member
    public Player Player;
    public float LineOfSight;
    public bool UsingMagic = false;

    // private member
    private bool m_FacingRight = true;
    public bool m_BottomTouched = false;
    private bool m_FrontTouched = false;
    private Rigidbody2D m_RigidBody2D;
    private BoxCollider2D m_BottomBox;
    private CircleCollider2D m_BottomLeft;
    private CircleCollider2D m_BottomRight;
    private BoxCollider2D m_RightBox;
    private bool m_Attacking = false;
    private bool m_ComboPossibility = false;
    private bool m_ComboValidated = false;
    private int m_AttackCount = 0;
    private bool m_Defending;
    private float m_Stamina = 100.0f;
    private float m_StaminaMax = 100.0f;
    private float m_StaminaMin = 0.0f;
    private bool m_BeingGroggy = false;
    private GameObject m_CounterSense;
    public float m_JetPackValue = 0.0f;
    private bool m_AttackLongCasting = false;
    private bool m_AttackLongCasted = false;
    public bool m_Countered = false;

    //variable
    private bool isAttacking;
    private float displacementSpeed = 90.0f;
    private bool _wasJumpDown = false;
    private bool _wasJumpUp = false;
    private bool _firstJump = false;
    private bool _firstJumpEnd = false;
    private bool _secondJump = false;
    private bool _secondJumpEnd = false;
    private bool _thirdJump = false;
    private LayerMask _wallsMask;
    private float _idleTimer = 0.0f;
    private bool _capJump = false;
    private bool _attackWasUp = true;
    private bool _bumped = false;
    private float _attackHoldTime = 0.0f;
    private float _counterTimer = 0.0f;
    private float _counterTimerMax = 2.0f;

    private bool hasDetected;
    private float AttackLength = 60.0f;


    // should be in some PlayerAnim script !!!
    Animator _anim;
    int _runHash = Animator.StringToHash("run");
    int _hitHash = Animator.StringToHash("hit");
    int _deathHash = Animator.StringToHash("death");
    int _idleHash = Animator.StringToHash("idle");
    int _DefendHash = Animator.StringToHash("defend");
    int _GroggyHash = Animator.StringToHash("isGroggy");
    int _attackLongHash = Animator.StringToHash("AttackLong");
    int _attackLongCastedHash = Animator.StringToHash("AttackLongCasted");
    int _attackLongReleasedHash = Animator.StringToHash("AttackLongReleased");
    int _counteredHash = Animator.StringToHash("Countered");

	// Use this for initialization
	void Start () {
        hasDetected = false;

        _anim = GetComponentInChildren<Animator>();
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        _wallsMask = LayerMask.GetMask("Walls");

        m_BottomBox = GetComponents<BoxCollider2D>()[1];
        m_BottomLeft = GetComponents<CircleCollider2D>()[0];
        m_BottomRight = GetComponents<CircleCollider2D>()[1];
        m_RightBox = GetComponents<BoxCollider2D>()[3];

	}
	
	// Update is called once per frame
	void Update () {
        var distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);
        if (distanceToPlayer < LineOfSight)
            hasDetected = true;
        else
            hasDetected = false;

        if (distanceToPlayer < AttackLength)
        {
            _anim.SetBool("Attack", true);
            m_Attacking = true;
        }
        else {
            _anim.SetBool("Attack", false);
            m_Attacking = false;
        }

        m_BottomTouched = m_BottomBox.IsTouchingLayers(_wallsMask) || m_BottomLeft.IsTouchingLayers(_wallsMask) || m_BottomRight.IsTouchingLayers(_wallsMask);

        if (m_Countered) {
            _counterTimer += 1 * Time.deltaTime;
            if (_counterTimer > _counterTimerMax) {
                _counterTimer = 0;
                m_Countered = false;
                _anim.SetBool(_counteredHash, false);
            }
        }
        
	}

    void FixedUpdate()
    {
        if (hasDetected == true && m_Attacking == false && m_Countered == false)
        {
            moveToward(Player.transform.position);
        }

        //Animation part
        _anim.SetFloat("velocityY", m_RigidBody2D.velocity.y);
        if (m_RigidBody2D.velocity.x != 0)
            _anim.SetBool(_runHash, true);
        else
            _anim.SetBool(_runHash, false);

        if (m_BottomTouched && !_anim.GetBool("jump"))
        {
            _anim.SetBool("grounded", true);
        }
        else
        {
            _anim.SetBool("grounded", false);
        }

        if (m_RigidBody2D.velocity.x > 0.1 && m_FacingRight)
            Flip();
        else if (m_RigidBody2D.velocity.x < -0.1 && !m_FacingRight)
            Flip();
    }

    void moveToward(Vector3 target) {
        Vector3 playerDir = target - transform.position;
        playerDir = playerDir.normalized;
        m_RigidBody2D.velocity = new Vector2(playerDir.x * displacementSpeed, m_RigidBody2D.velocity.y);
    }

    public void SetComboPossibility()
    {
        m_ComboPossibility = true;
    }

    public void ComboCheck()
    {
        if (m_ComboValidated)
        {
            _anim.SetBool("ComboValidated", true);
        }
        else
        {
            m_ComboValidated = false;
            _anim.SetBool("ComboValidated", false);
            m_AttackCount = 0;
        }
    }
    public void ResetComboValidated()
    {
        m_ComboValidated = false;
        _anim.SetBool("ComboValidated", false);
    }

    public void SetIdle(bool isIdle)
    {
        _anim.SetBool(_idleHash, isIdle);
        if (isIdle == false)
            _idleTimer = 0;
    }

    public void ResetBeingHit()
    {
        m_BeingHit = false;
    }

    public bool IsAttackLongCasted()
    {
        return m_AttackLongCasted;
    }
    void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 lScale = transform.localScale;
        lScale.x *= -1;
        transform.localScale = lScale;
    }

    public void startCounter() {
        m_Countered = true;
        _anim.SetBool(_counteredHash, true);
    }
}
