﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AssemblyCSharp;


public class Player : MonoBehaviour {

    public enum WeaponType
    {
        Sword,
        Stick
    }

    // public member
    public WeaponType CurrentWeapon = WeaponType.Sword;
    public float MoveSpeed = 800.0f;
	public float JumpSpeed = 1000.0f;
	public float MinJumpSpeed = 200.0f;
	public float JumpDelay = 0.03f;
    public float DashAttackSpeed = 100.0f;
    public float HitCoolDown = 0.5f;
	public float m_invicibleTimer = 0.5f;
	public float BumpCoolDown = 0.5f;
	[HideInInspector]
	public bool  HasJetPack = false;
	public float JetPackSpeed = 80.0f;
	public float JetPackDuration = 3.0f;
	public float JetPackMaxCoolDown = 6.0f;
	public ParticleSystem JetPackParticule;
	public float AttackLongCastDuration = 1.5f;
	public float AttackLongCastStartAfterTime = 1.5f;
    public float AttackLongDashSpeed = 50.0f;
    public float AttackLongDashDuration = 1.0f;
    public float CounterDelay = 0.2f;
    public float CounterAttackBackDashSpeed = 100.0f;
    public float CounterAttackForwardDashSpeed = 100.0f;
    public float BackDashSpeed = 50.0f;
    public float BackDashDuration = 1.0f;
    public float FlipLockTime = 0.2f;
    
    public WeaponScript Weapon;
	public float		AttackCoolDown = 0.15f;
    public float        SlowFactorWhileAttack = 0.5f;

    public GameObject   MagicShotPrefab;
    public float        MagicShotSpeed;
    public Vector2      MagicShotPositionOffsetFromPlayer = new Vector2(0, 0);

    public float 		TouchDetectionRadius = 0.2f;

	public float DefenseStat = 10.0f;
	public float StaminaConsommation = 10.0f;
	public float StaminaRegeneration = 10.0f;

	public ButtonScript BSMoveLeft;
	public ButtonScript BSMoveRight;
	public ButtonScript BSBackDash;
	public ButtonScript BSJump;
	public ButtonScript BSAttack;
	public ButtonScript BSDefend;
	public ButtonScript BSMagic;
	public ButtonScript BSWeaponSword;
	public ButtonScript BSWeaponStick;

	public GameObject stopWalkDust;
	public GameObject begingJumpDust;
	public GameObject endJumpDust;

	[HideInInspector]
	public Spell CurrentSpell;
	[HideInInspector]
	public bool UsingMagic = false;
	[HideInInspector]
	public float Mana = 100.0f;
	[HideInInspector]
	public float ManaMax = 100.0f;
	[HideInInspector]
	public float ManaMin = 0.0f;

	public float Health = 100.0f;
	[HideInInspector]
	public float HealthMax = 100.0f;
	[HideInInspector]
	public float HealthMin = 0.0f;

	public GameObject CounterFeedback;



	// private member
	private bool 				m_FacingRight = false;
	[HideInInspector]
	public bool 				m_BottomTouched =  false;
	private bool 				m_FrontTouched =  false;
	private Rigidbody2D 		m_RigidBody2D;
	private BoxCollider2D 		m_BottomBox;
	private CircleCollider2D 	m_BottomLeft;
	private CircleCollider2D 	m_BottomRight;
	private BoxCollider2D 		m_RightBox;
	[HideInInspector]
	public bool 				m_Attacking = false;
	private bool                m_ComboPossibility = false;
	private bool                m_ComboValidated = false;
	[HideInInspector]
	public int 					m_AttackCount = 0;
	[HideInInspector]
	public bool 				m_BeingHit = false;
	private bool 				m_invicible;
	private bool                m_Defending;
	private float               m_Stamina = 100.0f;
	private float               m_StaminaMax = 100.0f;
	private float               m_StaminaMin = 0.0f;
	private bool                m_BeingGroggy = false;
	private GameObject          m_CounterSense;
    private bool                m_Counter = false;
    private float               m_CounterTimer = 0.0f;
	[HideInInspector]
	public float 				m_JetPackValue = 0.0f;
	private bool                m_AttackLongCasting = false;
	private bool                m_AttackLongCasted = false;
	[HideInInspector]
	public bool                 m_AttackLongDashing = false;
    private bool                m_Bumped = false;
    private bool                m_BackDashing = false;
    private bool                m_Flipping = false;
    private int                 m_FlippingCount = 0;


    // variable
    private bool 		_wasJumpDown = false;
	private bool 		_wasJumpUp = false;
	private bool 		_firstJump =  false;
	private bool 		_firstJumpEnd =  false;
	private bool 		_secondJump =  false;
	private bool 		_secondJumpEnd =  false;
	private bool 		_thirdJump =  false;
	private LayerMask 	_wallsMask;
    private LayerMask   _enemiesMask;
	private LayerMask 	_mobilePlatformMask;
    private float 		_idleTimer = 0.0f;
	private	bool 		_capJump = false;
	private bool 		_attackWasUp = true;
	private float       _attackHoldTime = 0.0f;
	[HideInInspector]
	public bool        _isInCounterTime = false;
	[HideInInspector]
	public bool         _isCountering = false;
    private bool        _isDoubleJumpCollected = false;
	private bool        _isCounterCollected = false;
	private bool        _isJetPackCollected = false;
	private bool        _isChargedAttackCollected = false;
    private float       _move = 0;



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
	int _BackDashHash = Animator.StringToHash("BackDash");
	int _StopWalkHash = Animator.StringToHash("stopWalk");



    // Use this for initialization
    void Start () {
		_anim = GetComponentInChildren<Animator>();
		m_RigidBody2D = GetComponent<Rigidbody2D> ();
		_wallsMask = LayerMask.GetMask("Walls");
        _enemiesMask = LayerMask.GetMask("Enemy");
		_mobilePlatformMask = LayerMask.GetMask("mobilePlatform");

        m_BottomBox = GetComponents<BoxCollider2D> () [1];
		m_BottomLeft = GetComponents<CircleCollider2D> () [0];
		m_BottomRight = GetComponents<CircleCollider2D> () [1];
		m_RightBox = GetComponents<BoxCollider2D> () [3];

        // Init weapon
        if (CurrentWeapon == WeaponType.Stick)
        {
            CurrentWeapon = WeaponType.Stick;
            BSWeaponSword.gameObject.SetActive(false);
            BSWeaponStick.gameObject.SetActive(true);
            _anim.SetBool("Sword", false);
            _anim.SetBool("Stick", true);
        }
        else if (CurrentWeapon == WeaponType.Sword)
        {
            CurrentWeapon = WeaponType.Sword;
            BSWeaponStick.gameObject.SetActive(false);
            BSWeaponSword.gameObject.SetActive(true);
            _anim.SetBool("Stick", false);
            _anim.SetBool("Sword", true);
        }

        Weapon = GetComponentInChildren<WeaponScript>(true);
    }


	void Update()
    {
        //Gestion du idle
        _idleTimer += Time.deltaTime;

		if (m_RigidBody2D.velocity.x != 0 || m_RigidBody2D.velocity.y != 0 || m_BeingHit || m_BeingGroggy || m_Attacking)
		{
			SetIdle(false);
		}

		if (_idleTimer >= 2) {
			SetIdle(true);
		}

        //Gestion du backdash
        if ((BSBackDash.CurrentState == ButtonScript.ButtonState.Down || Input.GetButtonDown("BackDash")) && !m_BeingGroggy && !m_BeingHit && !UsingMagic && !m_Attacking && !m_Bumped && !m_BackDashing && !m_Defending && m_BottomTouched)
        {
            _anim.SetTrigger(_BackDashHash);
        }

        //Gestion des inputs de mouvements
        _move = 0.0f;
		bool stopWalk = _anim.GetBool(_StopWalkHash);
		//Input Mobile + Keyboard
		if ((BSMoveLeft.CurrentState == ButtonScript.ButtonState.Down || Input.GetKey(KeyCode.LeftArrow)) &&
			!m_BeingGroggy && !m_BeingHit && !UsingMagic && !m_Attacking && !m_Bumped && !m_BackDashing)
			_move = -1.0f;
		else if ((BSMoveRight.CurrentState == ButtonScript.ButtonState.Down || Input.GetKey(KeyCode.RightArrow)) && 
			!m_BeingGroggy && !m_BeingHit && !UsingMagic && !m_Attacking && !m_Bumped && !m_BackDashing)
			_move = 1.0f;



        if (_move > 0.1 && m_FacingRight)
            Flip();
        else if (_move < -0.1 && !m_FacingRight)
            Flip();


        if (m_AttackLongDashing)
            return;

        //Animation part
        _anim.SetFloat("velocityX", Mathf.Abs(m_RigidBody2D.velocity.x));
        _anim.SetFloat("velocityY", m_RigidBody2D.velocity.y);
        if (Mathf.Abs(_move) >= 0.1)
			_anim.SetBool(_runHash, true);
		else
			_anim.SetBool(_runHash, false);

		if (m_BottomTouched && !_anim.GetBool ("jump"))
		{
			_anim.SetBool("grounded", true);
		}
		else {
			_anim.SetBool("grounded", false);
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

				Vector3 newScale = transform.localScale;
				newScale.x = -newScale.x;
				endJumpDust.transform.localScale = newScale;
				Vector3 newPos = transform.position;
				newPos.y -= 10;
				newPos.x += -10 * newScale.x;
				begingJumpDust.transform.position = newPos;
				begingJumpDust.SetActive (true);
				//m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, JumpSpeed);

				StartCoroutine (Jump (JumpSpeed));
			} else if (_firstJumpEnd && !_secondJump && _isDoubleJumpCollected) {
				_secondJump = true;
				_secondJumpEnd = false;
				// second jump
				//m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, JumpSpeed);
				StartCoroutine (Jump (JumpSpeed));
			} else if (HasJetPack && _secondJumpEnd && _isJetPackCollected) {
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
        
		if (isDefendDown && m_Stamina > 0 && !m_BeingGroggy && !m_BeingHit && !m_Attacking && _isCounterCollected)
		{
			m_Defending = true;
			_anim.SetBool("Defend", true);
		}

		if (isDefendUp && _isCounterCollected)
		{
			m_Defending = false;
			_anim.SetBool("Defend", false);
            m_CounterTimer = 0;
            _isInCounterTime = false;
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

        //Gestion du contre
        bool isCounterWasUp = Input.GetButtonDown("Defend");

		if (isCounterWasUp && _isCounterCollected)
            _isInCounterTime = true;

        if (_isInCounterTime) {
            //Time.timeScale = 0.1f;
            m_CounterTimer += 1 * Time.deltaTime;
            if (m_CounterTimer > CounterDelay) {
                m_CounterTimer = 0;
                _isInCounterTime = false;
            }
                
        }

		//Gestion de l'attaque
		if (BSAttack.CurrentState != ButtonScript.ButtonState.Down && Input.GetAxis("Attack") != 1)
		{
			_attackWasUp = true;
            if(m_AttackLongCasted)
            {
                _anim.SetBool(_attackLongReleasedHash, true);
                Invoke("ResetAttackLongCasted", 0.2f);
            }
            else
            {
                m_AttackLongCasting = false;
                _anim.SetBool(_attackLongHash, false);
                _anim.SetBool(_attackLongCastedHash, false);
                _anim.SetBool(_attackLongReleasedHash, false);
            }
		}
		else if (_attackWasUp && m_AttackCount < 3 && m_Stamina - Weapon.StaminaConsomation > m_StaminaMin && (m_AttackCount == 0 || m_ComboPossibility == true) && !m_Defending && !m_BeingHit)
		{
            Debug.Log("Attack !");
            SetIdle(false);
			_attackWasUp = false;
			++m_AttackCount;
			_attackHoldTime = 0.0f;


            if (m_AttackCount == 1)
			{
                Debug.Log("Attack Count 1");
				_anim.SetBool("Attack", true);
				m_Attacking = true;
				m_Stamina -= Weapon.StaminaConsomation;
				PlayerDefend DefendEvent = new PlayerDefend(m_Stamina);
				Events.instance.Raise(DefendEvent);
            }
			else if (m_AttackCount == 2)
			{
                Debug.Log("Attack Count 2");

                m_ComboValidated = true;
				m_ComboPossibility = false;

                _anim.SetBool("ComboValidated", true);
            }
			else if (m_AttackCount == 3 && CurrentWeapon == WeaponType.Sword)
			{
				Debug.Log("Attack Count 3");
				m_ComboValidated = true;
				m_ComboPossibility = false;

                _anim.SetBool("ComboValidated", true);
            }
		}
		else if(!m_Defending && !m_BeingHit)
		{
			_attackHoldTime += Time.deltaTime;
			if(_attackHoldTime > AttackLongCastStartAfterTime && _isChargedAttackCollected)
			{
				m_AttackLongCasting = true;
                m_AttackCount = 0;

                _anim.SetBool(_attackLongHash, true);
                _anim.SetBool("Attack", false);
                if (_attackHoldTime > AttackLongCastDuration)
				{
					m_AttackLongCasted = true;
					_anim.SetBool(_attackLongCastedHash, true);
				}
			}
		}

		//Gestion de la magie
		bool isMagicDown = Input.GetButton("Magic") || (BSMagic.CurrentState == ButtonScript.ButtonState.Down);
		bool isMagicUp = Input.GetButtonUp("Magic") || (BSMagic.CurrentState == ButtonScript.ButtonState.Up);

		if (isMagicDown && !m_BeingGroggy && !m_BeingHit && !m_Attacking && !UsingMagic)
		{
			UsingMagic = true;
			CurrentSpell.launchSort();
		}

        //Gestion du switch d'arme
        if(BSWeaponSword.CurrentState == ButtonScript.ButtonState.Down)
        {
            SwitchWeaponTo(WeaponType.Stick);
        }
        else if(BSWeaponStick.CurrentState == ButtonScript.ButtonState.Down)
        {
            SwitchWeaponTo(WeaponType.Sword);
        }

    }

	void FixedUpdate () 
    {

        if (m_BeingHit)
            return;

		// Evalute states
		m_BottomTouched = m_BottomBox.IsTouchingLayers (_wallsMask) || m_BottomLeft.IsTouchingLayers (_wallsMask) || m_BottomRight.IsTouchingLayers (_wallsMask)
			|| m_BottomBox.IsTouchingLayers(_enemiesMask) || m_BottomLeft.IsTouchingLayers(_enemiesMask) || m_BottomRight.IsTouchingLayers(_enemiesMask) || m_BottomBox.IsTouchingLayers (_mobilePlatformMask);
		m_FrontTouched = /*_left_box.IsTouchingLayers (wallsMask) ||*/ m_RightBox.IsTouchingLayers (_wallsMask) || m_BottomRight.IsTouchingLayers (_wallsMask);
		bool platformMobile = m_BottomBox.IsTouchingLayers (_mobilePlatformMask);

		if (m_BottomTouched) {
			_firstJump = false;
			_secondJump = false;

			if (m_JetPackValue > 0) {
				m_JetPackValue -= Time.fixedDeltaTime * (JetPackDuration / JetPackMaxCoolDown);
				m_JetPackValue = Mathf.Max (m_JetPackValue, 0.0f);		
				PlayerJetpackValueChanged playerJetpackValEvent = new PlayerJetpackValueChanged(m_JetPackValue, JetPackDuration);
				Events.instance.Raise(playerJetpackValEvent);
			}

			JetPackParticule.Stop();
		} else if (_thirdJump && m_JetPackValue < JetPackDuration) {
			// Jetpack
			m_JetPackValue += Time.fixedDeltaTime;
			m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, JetPackSpeed / Mathf.Min (Mathf.Max (m_JetPackValue, 0.2f), 1.0f));
			JetPackParticule.Play();

			PlayerJetpackValueChanged playerJetpackValEvent = new PlayerJetpackValueChanged(m_JetPackValue, JetPackDuration);
			Events.instance.Raise(playerJetpackValEvent);
		} else {
			_thirdJump = false;
			JetPackParticule.Stop();
		}


        if (m_AttackLongDashing || m_Flipping)
            return;
		if ((m_AttackCount>0 || m_AttackLongCasting) && m_BottomTouched) {
            _move *= SlowFactorWhileAttack;
		}




		//Debug.Log (m_RigidBody2D.velocity);
		// classic move
		if (Mathf.Abs (_move) > 0) {

			if (m_BackDashing) {
				return;
			}

			if (m_Defending && m_BottomTouched) {
				if (!m_FrontTouched) {
					m_RigidBody2D.velocity = new Vector2 (_move * MoveSpeed / 2, m_RigidBody2D.velocity.y);
				} else if (m_FacingRight && _move > 0) {
					m_RigidBody2D.velocity = new Vector2 (_move * MoveSpeed / 2, m_RigidBody2D.velocity.y);
				} else if (!m_FacingRight && _move < 0) {
					m_RigidBody2D.velocity = new Vector2 (_move * MoveSpeed / 2, m_RigidBody2D.velocity.y);
				}
			} else {
				if (!m_FrontTouched) {
					m_RigidBody2D.velocity = new Vector2 (_move * MoveSpeed, m_RigidBody2D.velocity.y);
				}
			}
			
		}

		else if (m_Attacking)
			return;
		
		else if(!platformMobile) 
			m_RigidBody2D.velocity = new Vector2 (0, m_RigidBody2D.velocity.y);

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
        m_Flipping = true;
        ++m_FlippingCount;
        Invoke("ResetFlipping", FlipLockTime);

		m_FacingRight = !m_FacingRight;
		Vector3 lScale = transform.localScale;
		lScale.x *= -1;
		transform.localScale = lScale;

        m_RigidBody2D.velocity = new Vector2(0, m_RigidBody2D.velocity.y);
	}

    void ResetFlipping()
    {
        --m_FlippingCount;
        if (m_FlippingCount <= 0)
            m_Flipping = false;
    }



    public void ResetAttackTrippleAnim() {
		_anim.SetBool ("Attack", false);
		_anim.SetBool ("AttackDouble", false);
		_anim.SetBool ("AttackTripple", false);
		m_Attacking = false;
	}

	public void Hit (MonoBehaviour iSource, float iDamageValue, float iStaminaLossPerHit) {
        if (m_AttackLongDashing)
            return;

        if(_isInCounterTime)
        {
            if(iSource is Enemy)
            {
                Enemy e = (Enemy)iSource;
                e.GetCountered();
                _anim.SetBool("CounterEnemy", true);
                _isCountering = true;
                Invoke("ResetCounterEnemy", 2.0f);
            }
            else if (iSource.GetType() == typeof(Shot))
            {
                Shot s = (Shot)iSource;
                s.GetCountered();
            }

            _isInCounterTime = false; // to counter only one Hit
        }
		else if (!m_BeingHit && !_isCountering && !m_invicible) {
			m_BeingHit = true;
			Debug.Log ("hit");
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
				_anim.SetTrigger(_hitHash);
				Events.instance.Raise(_playerHitEvent);
				ResetAttackTrippleAnim();
				m_AttackCount = 0;
				if (Health <= 0)
				{
					_anim.SetTrigger(_deathHash);
					this.enabled = false;
				}
                m_RigidBody2D.AddForce(new Vector2(0, 8000), ForceMode2D.Force); // en vrai 8000 c'est cool
			}
			Invoke("ResetBeingHit", HitCoolDown);

		}
	}

	public void Bump(Vector3 iSourcePosition, float iBumpForce) {
		if (!m_Bumped)
		{
			//Vector2 bumpDir = this.transform.position.x > iSourcePosition.x ? new Vector2(iBumpForce, iBumpForce) : new Vector2(-iBumpForce, iBumpForce);
			Vector3 correctPlayerPosition = transform.position;
			correctPlayerPosition.y += 40;
			Vector2 bumpDir = correctPlayerPosition - iSourcePosition;
			this.m_RigidBody2D.velocity = bumpDir.normalized	* iBumpForce;
			m_Bumped = true;
			Invoke("ResetBump", BumpCoolDown);
		}
	}

    public void DoHitEnemy(Enemy iEnemy)
    {
        WeaponScript ws = this.GetComponentInChildren<WeaponScript>();

        if (m_AttackLongCasted || _isCountering)
        {
            iEnemy.Hit(ws.LongAttackDamageValue);
            if (iEnemy.IsBumpable)
                iEnemy.Bump(this.transform.position, iEnemy.BumpForce*3);
        }
        else
        {
            iEnemy.Hit(ws.DamageValue);
            if (iEnemy.IsBumpable)
                iEnemy.Bump(this.transform.position, iEnemy.BumpForce);
        }

    }

	public void ResetBump() {
		m_Bumped = false;
	}

	public void SetIdle(bool isIdle) {
		_anim.SetBool(_idleHash,isIdle);
		if(isIdle == false)
			_idleTimer = 0;
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        Coin otherCoin = other.gameObject.GetComponent<Coin>();
		if (otherCoin != null)
		{
			PlayerLoot lootEvent = new PlayerLoot (otherCoin.gameObject);
			Events.instance.Raise (lootEvent);
			otherCoin.isTook ();
		}
		
	}


	public void ResetBeingHit() {
		m_BeingHit = false;
		m_invicible = true;
		Invoke ("ResetInvicible",m_invicibleTimer);
	}

	public void ResetInvicible() {
		m_invicible = false;
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

	private void ResetAttackLongCasted()
    {
        m_AttackLongCasted = false;
    }

    public bool IsAttackLongCasted()
    {
        return m_AttackLongCasted;
    }

    public void SwitchWeaponTo(WeaponType iWeapon)
    {
        if (iWeapon == WeaponType.Stick && CurrentWeapon!=WeaponType.Stick)
        {
            CurrentWeapon = WeaponType.Stick;
            BSWeaponSword.CurrentState = ButtonScript.ButtonState.Up;
            BSWeaponSword.gameObject.SetActive(false);
            BSWeaponStick.gameObject.SetActive(true);
            _anim.SetBool("Sword", false);
            _anim.SetBool("Stick", true);
        }
        else if (iWeapon == WeaponType.Sword && CurrentWeapon != WeaponType.Sword)
        {
            CurrentWeapon = WeaponType.Sword;
            BSWeaponStick.CurrentState = ButtonScript.ButtonState.Up;
            BSWeaponStick.gameObject.SetActive(false);
            BSWeaponSword.gameObject.SetActive(true);
            _anim.SetBool("Stick", false);
            _anim.SetBool("Sword", true);
        }
    }


    public void setCounter(){
        m_Counter = true;
    }
    

    public void FireStick()
    {
        GameObject shotInstance = (GameObject)Instantiate(MagicShotPrefab);
        MagicShot shot = shotInstance.GetComponent<MagicShot>();
        shot.Source = this.gameObject;
        shotInstance.transform.position = new Vector3(this.transform.position.x + MagicShotPositionOffsetFromPlayer.x, this.transform.position.y + MagicShotPositionOffsetFromPlayer.y, this.transform.position.z);

        if(m_FacingRight)
            shot.MoveVector = new Vector3(-MagicShotSpeed, 0, 0);
        else
            shot.MoveVector = new Vector3(MagicShotSpeed, 0, 0);
    }

	public void DashForward()
    {
        m_AttackLongDashing = true;
        if(m_FacingRight)
            m_RigidBody2D.velocity = new Vector2(-AttackLongDashSpeed, 0);
        else
            m_RigidBody2D.velocity = new Vector2(AttackLongDashSpeed, 0);

        Invoke("ResetAttackLongDash", AttackLongDashDuration);
    }

    public void BackDash()
    {
       m_BackDashing = true;
       m_RigidBody2D.velocity = new Vector2(0, 0);

        if (m_FacingRight)
            m_RigidBody2D.AddForce(new Vector2(BackDashSpeed,0),ForceMode2D.Impulse);
        else
            m_RigidBody2D.AddForce(new Vector2(-BackDashSpeed, 0), ForceMode2D.Impulse);
        Invoke("ResetBackDash",BackDashDuration);
    }

    public void Dash(float iSpeed)
	{
		if (m_BottomTouched) {
			if(m_FacingRight)
				m_RigidBody2D.velocity = new Vector2(-iSpeed, m_RigidBody2D.velocity.y);
			else
				m_RigidBody2D.velocity = new Vector2(iSpeed, m_RigidBody2D.velocity.y);
		}
	}

    public void ResetAttackLongDash()
    {
        m_AttackLongDashing = false;
        m_RigidBody2D.velocity = new Vector2(0, 0);
    }

    public void ResetBackDash()
    {
        m_RigidBody2D.velocity = new Vector2(0, 0);
        StartCoroutine(delayBackDash());
    }

    IEnumerator delayBackDash() {
        yield return new WaitForSeconds(0.2f);
        m_BackDashing = false;
    }

    public void setDoubleJump(bool Bool){
		_isDoubleJumpCollected = Bool;
	}

	public void setChargedAttack(bool Bool){
		_isChargedAttackCollected = Bool;
	}

	public void setCounter(bool Bool){
		_isCounterCollected = Bool;
	}

	public void setJetPack(bool Bool){
		_isJetPackCollected = Bool;
	}

    public void ResetCounterEnemy()
    {
        _anim.SetBool("CounterEnemy", false);
        _isCountering = false;
    }
}
