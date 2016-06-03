using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AssemblyCSharp;


public class Player : MonoBehaviour {

	// public member
	public float MoveSpeed = 800.0f;
	public float JumpSpeed = 1000.0f;
	public float MinJumpSpeed = 200.0f;
	public float JumpDelay = 0.19f;
	public float HitCoolDown = 0.5f;
	public bool  HasJetPack = false;
	public float JetPackSpeed = 80.0f;

	public ButtonScript BSMoveLeft;
	public ButtonScript BSMoveRight;
	public ButtonScript BSJump;
	public ButtonScript BSAttack;
	public ButtonScript BSDefend;
    public ButtonScript BSMagic; 

	public WeaponScript Weapon;
	public float		AttackCoolDown = 0.15f;

	public float 		TouchDetectionRadius = 0.2f;

    public float DefenseStat = 10.0f;
    public float StaminaConsommation = 10.0f;
    public float StaminaRegeneration = 10.0f;

    public Spell CurrentSpell;
    public bool UsingMagic = false;
    public float Mana = 100.0f;
    public float ManaMax = 100.0f;
    public float ManaMin = 0.0f;

    public float Health = 100.0f;
    public float HealthMax = 100.0f;
    public float HealthMin = 0.0f;

    public GameObject CounterFeedback;



	// private member
	private bool 				m_FacingRight = false;
	public bool 				m_BottomTouched =  false;
	private bool 				m_FrontTouched =  false;
	private Rigidbody2D 		m_RigidBody2D;
	private BoxCollider2D 		m_BottomBox;
	private CircleCollider2D 	m_BottomLeft;
	private CircleCollider2D 	m_BottomRight;
	//private BoxCollider2D 		m_LeftBox;
	private BoxCollider2D 		m_RightBox;
	private bool 				m_Attacking = false;
    private bool                m_ComboPossibility = false;
    private bool                m_ComboValidated = false;
	private int 				m_AttackCount = 0;
	private bool 				m_BeingHit = false;
    private bool                m_Defending;
    private float               m_Stamina = 100.0f;
    private float               m_StaminaMax = 100.0f;
    private float               m_StaminaMin = 0.0f;
    private bool                m_BeingGroggy = false;
    private GameObject          m_counterSense;
    
    
    


	// variable
	private bool 		_wasJumpDown = false;
	private bool 		_wasJumpUp = false;
	private bool 		_firstJump =  false;
	private bool 		_firstJumpEnd =  false;
	private bool 		_secondJump =  false;
	private bool 		_secondJumpEnd =  false;
	private bool 		_thirdJump =  false;
	private LayerMask 	_wallsMask;
	private float 		_idleTimer = 0.0f;
	private	bool 		_capJump = false;
	private float 		_deltaFromLastAttack = 0.0f;
	private bool 		_attackWasUp = true;



	// should be in some PlayerAnim script !!!
	Animator _anim;
	int runHash = Animator.StringToHash("run");
	int hitHash = Animator.StringToHash("hit");
	int deathHash = Animator.StringToHash("death");
	int idleHash = Animator.StringToHash("idle");
    int _DefendHash = Animator.StringToHash("defend");
    int _GroggyHash = Animator.StringToHash("isGroggy");
    

	//Animator _AnimatorSwordCollideR;


	// Use this for initialization
	void Start () {
		_anim = GetComponentInChildren<Animator>();
        Weapon = GetComponentInChildren<WeaponScript>();
		//_AnimatorSwordCollideR = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		m_RigidBody2D = GetComponent<Rigidbody2D> ();
		_wallsMask = LayerMask.GetMask("Walls");

		m_BottomBox = GetComponents<BoxCollider2D> () [1];
		m_BottomLeft = GetComponents<CircleCollider2D> () [0];
		m_BottomRight = GetComponents<CircleCollider2D> () [1];
		//m_LeftBox = GetComponents<BoxCollider2D> () [2];
		m_RightBox = GetComponents<BoxCollider2D> () [3];

	}



	//private bool _wasJumpDown = false;

	void Update() {
        //Time.timeScale = 0.1f;
        //Idle timer
        _idleTimer += Time.deltaTime;

        if (m_RigidBody2D.velocity.x != 0 || m_RigidBody2D.velocity.y != 0 || m_BeingHit || m_BeingGroggy || m_Attacking)
        {
            SetIdle(false);
        }

        if (_idleTimer >= 2) {
            SetIdle(true);
        }

        // Gestion du jump
        bool isJumpDown = false;
        bool isJumpUp = false;

		isJumpDown = Input.GetButtonDown ("Jump") || (BSJump.CurrentState == ButtonScript.ButtonState.Down && !_wasJumpDown);
		isJumpUp = Input.GetButtonUp ("Jump") || (BSJump.CurrentState == ButtonScript.ButtonState.Up && !_wasJumpUp);



        if (isJumpDown && !m_BeingGroggy && !m_BeingHit && !m_Attacking)
        {
			_capJump = false;
			_wasJumpDown = true;
			_wasJumpUp = false;
			if (!_firstJump && m_BottomTouched) {
				_firstJump = true;
				_firstJumpEnd = false;

				//m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, JumpSpeed);
				StartCoroutine (Jump (JumpSpeed));
			} else if (_firstJumpEnd && !_secondJump) {
				_secondJump = true;
				_secondJumpEnd = false;
				// second jump
				//m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, JumpSpeed);
				StartCoroutine (Jump (JumpSpeed));
			} else if (HasJetPack && _secondJumpEnd) {
				_thirdJump = true;
			}
		}
		else if(isJumpUp) {
			_wasJumpDown = false;
			_wasJumpUp = true;
			_firstJump = false;
			_firstJumpEnd = true;
			_secondJumpEnd = true;
			_capJump = true;
			_thirdJump = false;

			if(m_RigidBody2D.velocity.y > MinJumpSpeed)
				m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, MinJumpSpeed);
				//StartCoroutine (Jump (MinJumpSpeed));
		}

        //Gestion de la défense
        bool isDefendDown = Input.GetButton("Defend") || (BSDefend.CurrentState == ButtonScript.ButtonState.Down);
        bool isDefendUp = Input.GetButtonUp("Defend") || (BSDefend.CurrentState == ButtonScript.ButtonState.Up);
        if (isDefendDown && m_Stamina > 0 && !m_BeingGroggy && !m_BeingHit && !m_Attacking)
        {
            m_Defending = true;
            _anim.SetBool("Defend", true);
        }

        if (isDefendUp)
        {
            m_Defending = false;
            _anim.SetBool("Defend", false);
        }

        if (m_Defending)
        {
            if (m_Stamina < m_StaminaMin) {
                m_Stamina = 0;
                return;
            }
            m_Stamina -= StaminaConsommation * Time.deltaTime;
            PlayerDefend DefendEvent = new PlayerDefend(m_Stamina);
            Events.instance.Raise(DefendEvent);
        }
        else if( m_Stamina < m_StaminaMax)
        {
            m_Stamina += StaminaRegeneration * Time.deltaTime;
            if (m_Stamina > m_StaminaMax)
                m_Stamina = m_StaminaMax;
            PlayerDefend DefendEvent = new PlayerDefend(m_Stamina);
            Events.instance.Raise(DefendEvent);
        }

        //Gestion de l'attaque
        float TestStamina = m_Stamina - Weapon.StaminaConsomation;

        if (BSAttack.CurrentState != ButtonScript.ButtonState.Down && Input.GetAxis("Attack") != 1)
        {
            _attackWasUp = true;
        }
        else if (_attackWasUp && m_AttackCount < 3 && TestStamina > m_StaminaMin && (m_AttackCount == 0 || m_ComboPossibility == true) && !m_Defending && !m_BeingHit)
        {
            SetIdle(false);
            _attackWasUp = false;
            ++m_AttackCount;
            _deltaFromLastAttack = 0;

            if (m_AttackCount == 1)
            {
                _anim.SetBool("Attack", true);
                m_Attacking = true;
                m_Stamina -= Weapon.StaminaConsomation;
                PlayerDefend DefendEvent = new PlayerDefend(m_Stamina);
                Events.instance.Raise(DefendEvent);
            }
            else if (m_AttackCount == 2)
            {
                m_ComboValidated = true;
                m_ComboPossibility = false;
            }
            else if (m_AttackCount == 3)
            {
                m_ComboValidated = true;
                m_ComboPossibility = false;
            };

        }

        //Gestion de la magie
        bool isMagicDown = Input.GetButton("Magic") || (BSMagic.CurrentState == ButtonScript.ButtonState.Down);
        bool isMagicUp = Input.GetButtonUp("Magic") || (BSMagic.CurrentState == ButtonScript.ButtonState.Up);

        if (isMagicDown && !m_BeingGroggy && !m_BeingHit && !m_Attacking && !UsingMagic)
        {
            UsingMagic = true;
            CurrentSpell.launchSort();
        }
        Debug.Log(UsingMagic);
	}

	void FixedUpdate () {

		float move = 0.0f;

		if (BSMoveLeft.CurrentState == ButtonScript.ButtonState.Down && !m_BeingGroggy && !m_BeingHit && !UsingMagic)
			move = -1.0f;
        else if (BSMoveRight.CurrentState == ButtonScript.ButtonState.Down && !m_BeingGroggy && !m_BeingHit && !UsingMagic)
			move = 1.0f;
        else if (!m_BeingGroggy && !m_BeingHit && !UsingMagic)
			move = Input.GetAxis ("Horizontal");

		// Evalute states
		m_BottomTouched = m_BottomBox.IsTouchingLayers (_wallsMask) || m_BottomLeft.IsTouchingLayers (_wallsMask) || m_BottomRight.IsTouchingLayers (_wallsMask);
		m_FrontTouched = /*_left_box.IsTouchingLayers (wallsMask) ||*/ m_RightBox.IsTouchingLayers (_wallsMask) || m_BottomRight.IsTouchingLayers (_wallsMask);

		if (m_BottomTouched) {
			_firstJump = false;
			_secondJump = false;
		} else if (_thirdJump) {
			// Jetpack
			m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, JetPackSpeed);
		}

		if (m_AttackCount > 0)
			_deltaFromLastAttack += Time.fixedDeltaTime;

     

		if (m_AttackCount>0) {
			if(m_BottomTouched)
				m_RigidBody2D.velocity = Vector2.zero;
			move = 0;
		}


		//Animation part
		_anim.SetFloat("velocityY", m_RigidBody2D.velocity.y);
		if(move != 0)
			_anim.SetBool(runHash, true);
		else
			_anim.SetBool(runHash, false);

		if (m_BottomTouched && !_anim.GetBool ("jump"))
        {
            _anim.SetBool("grounded", true);
        }
        else {
            _anim.SetBool("grounded", false);
        }




		// classic move
		if (Mathf.Abs (move) > 0) {
            if (m_Defending)
            {
                if (!m_FrontTouched)
                {
                    m_RigidBody2D.velocity = new Vector2(move * MoveSpeed/2, m_RigidBody2D.velocity.y);
                }
                else if (m_FacingRight && move > 0)
                {
                    m_RigidBody2D.velocity = new Vector2(move * MoveSpeed/2, m_RigidBody2D.velocity.y);
                }
                else if (!m_FacingRight && move < 0)
                {
                    m_RigidBody2D.velocity = new Vector2(move * MoveSpeed/2, m_RigidBody2D.velocity.y);
                }
            }
            else {
                if (!m_FrontTouched)
                {
                    m_RigidBody2D.velocity = new Vector2(move * MoveSpeed, m_RigidBody2D.velocity.y);
                }
                else if (m_FacingRight && move > 0)
                {
                    m_RigidBody2D.velocity = new Vector2(move * MoveSpeed, m_RigidBody2D.velocity.y);
                }
                else if (!m_FacingRight && move < 0)
                {
                    m_RigidBody2D.velocity = new Vector2(move * MoveSpeed, m_RigidBody2D.velocity.y);
                }
            }
			
		}

		if(m_RigidBody2D.velocity.x > 0.1  && m_FacingRight)
			Flip ();				
		else if (m_RigidBody2D.velocity.x  < -0.1 && !m_FacingRight)
			Flip ();
	}

	private IEnumerator Jump(float iJumpSpeed) {
		_anim.SetBool ("jump", true);
		_anim.SetBool("grounded", false);
		yield return new WaitForSeconds(JumpDelay);
		if (_capJump)
			m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, MinJumpSpeed);
		else
			m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, iJumpSpeed);
		_anim.SetBool ("jump", false);
	}

	void Flip() {
		m_FacingRight = !m_FacingRight;
		Vector3 lScale = transform.localScale;
		lScale.x *= -1;
		transform.localScale = lScale;
	}


	void ResetAttackTrippleAnim() {
		_anim.SetBool ("Attack", false);
		_anim.SetBool ("AttackDouble", false);
		_anim.SetBool ("AttackTripple", false);
        m_Attacking = false;
	}

	public void Hit (float iDamageValue, float iStaminaLossPerHit){
        
        
        if (!m_BeingHit) {
			m_BeingHit = true;

            if (m_Defending)
            {
                iDamageValue -= DefenseStat;
                m_Stamina -= iStaminaLossPerHit;
                if (m_Stamina < 0)
                {
                    m_Stamina = 0;
                    m_BeingGroggy = true;
                    _anim.SetBool(_GroggyHash, true);
                    m_Defending = false;
                    _anim.SetBool("Defend", false);
                    Invoke("ResetGroggy", 2.0f);
                }
                Health -= iDamageValue;
            }
            else {
                Health -= iDamageValue;
                PlayerHit _playerHitEvent = new PlayerHit(Health);
                _anim.SetTrigger(hitHash);
                Events.instance.Raise(_playerHitEvent);
                ResetAttackTrippleAnim();
                m_AttackCount = 0;
                if (Health <= 0)
                {
                    _anim.SetTrigger(deathHash);
                    this.enabled = false;
                }
            }
            Invoke("ResetBeingHit", HitCoolDown);

		}
	}

	public void Bump(Vector3 iSourcePosition, float iBumpForce) {
		Vector2 bumpDir = this.transform.position.x>iSourcePosition.x? new Vector2(iBumpForce,iBumpForce) : new Vector2(-iBumpForce,iBumpForce);

		this.m_RigidBody2D.velocity += bumpDir;
	}

    public void SetIdle(bool isIdle) {
        _anim.SetBool(idleHash,isIdle);
        if(isIdle == false)
            _idleTimer = 0;
    }

    void OnTriggerEnter2D(Collider2D other) {
//        Enemy otherEnemy = other.gameObject.GetComponent<Enemy> ();
//		if (otherEnemy != null) {
//			Hit (otherEnemy.DamagePerHit);
//
//			//Bump player
//			Vector3 bumpDirection = this.transform.position - otherEnemy.transform.position;
//		}

        CoinScript otherCoin = other.gameObject.GetComponent<CoinScript>();
        if (otherCoin != null)
        {
			PlayerLoot lootEvent = new PlayerLoot (otherCoin.gameObject);
			Events.instance.Raise (lootEvent);
            Vector3 lootPosition = transform.position;
            lootPosition.y += 50f;
            otherCoin.PopLootText(lootPosition);
            otherCoin.CoinAnim.destroy();
        }
        
    }


	public void ResetBeingHit() {
		m_BeingHit = false;
	}

    public void ResetGroggy()
    {
        _anim.SetBool(_GroggyHash,false);
        m_BeingGroggy = false;
    }

     public void SetComboPossibility() {
         m_ComboPossibility = true;
    }

     public void ComboCheck()
     {
         Debug.Log("test");
         if (m_ComboValidated)
         {
             _anim.SetBool("ComboValidated", true);
         }
         else {
             m_ComboValidated = false;
             _anim.SetBool("ComboValidated", false);
             ResetAttackTrippleAnim();
             m_AttackCount = 0;
         }
     }
     public void ResetComboValidated()
     {
         m_ComboValidated = false;
         _anim.SetBool("ComboValidated", false);
         m_Stamina -= Weapon.StaminaConsomation;
         PlayerDefend DefendEvent = new PlayerDefend(m_Stamina);
         Events.instance.Raise(DefendEvent);
    }

		
}
