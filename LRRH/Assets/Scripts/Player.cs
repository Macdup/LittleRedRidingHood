using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AssemblyCSharp;

// should have its own script or be in GameEvent.cs !
public class PlayerHit : GameEvent{
	public float Health;
	public PlayerHit(float iHealth){
		Health = iHealth;
	}
}

public class Player : MonoBehaviour {

	// public member
	public float MoveSpeed = 800.0f;
	public float JumpSpeed = 1000.0f;
	public float MinJumpSpeed = 200.0f;
	public float JumpDelay = 0.8f;
	public float Health = 100.0f;

	public ButtonScript BSMoveLeft;
	public ButtonScript BSMoveRight;
	public ButtonScript BSJump;
	public ButtonScript BSFireA;
	public ButtonScript BSFireB;   

	public GameObject 	Weapon;
	//public float 		WeaponDuration = 1000.0f;
	public float 		WeaponCoolDown = 0.3f;

	public float 		TouchDetectionRadius = 0.2f;



	// private member
	private bool 				m_FacingRight = false;
	private bool 				m_BottomTouched =  false;
	private bool 				m_FrontTouched =  false;
	private Rigidbody2D 		m_RigidBody2D;
	private BoxCollider2D 		m_BottoBox;
	private CircleCollider2D 	m_BottomLeft;
	private CircleCollider2D 	m_BottomRight;
	private BoxCollider2D 		m_LeftBox;
	private BoxCollider2D 		m_RightBox;

	// variable
	private bool 		_firstJump =  false;
	private bool 		_firstJumpEnd =  false;
	private bool 		_secondJump =  false;
	private bool 		_weapon = false;
	private LayerMask 	_wallsMask;
	private float 		_idleTimer = 0.0f;
	public bool 		_frontHitOn = false;
	public float 		_moveDeb;




	// should be in some PlayerAnim script !!!
	Animator _anim;
	int runHash = Animator.StringToHash("run");
	int jumpHash = Animator.StringToHash("jump");
	int hitHash = Animator.StringToHash("hit");
	int deathHash = Animator.StringToHash("death");
	int groundedHash = Animator.StringToHash("grounded");
	int idleHash = Animator.StringToHash("idle");


	// Use this for initialization
	void Start () {

		// FOR DEBUG
		//Time.timeScale = 0.3f;

		_anim = GetComponentInChildren<Animator>();
		m_RigidBody2D = GetComponent<Rigidbody2D> ();
		_wallsMask = LayerMask.GetMask("Walls");

		m_BottoBox = GetComponents<BoxCollider2D> () [1];
		m_BottomLeft = GetComponents<CircleCollider2D> () [0];
		m_BottomRight = GetComponents<CircleCollider2D> () [1];
		m_LeftBox = GetComponents<BoxCollider2D> () [2];
		m_RightBox = GetComponents<BoxCollider2D> () [3];
	}



	//private bool _wasJumpDown = false;

	void Update() {
		bool isJumpDown = false;
		bool isJumpUp = false;

        //Idle timer
        _idleTimer += Time.deltaTime;

        if (m_RigidBody2D.velocity.x != 0 || m_RigidBody2D.velocity.y != 0)
        {
            SetIdle(false);
        }

        if (_idleTimer >= 2) {
            SetIdle(true);
        }

		isJumpDown = Input.GetButtonDown ("Jump") || (BSJump.CurrentState == ButtonScript.ButtonState.Down);
		isJumpUp = Input.GetButtonUp ("Jump") || (BSJump.CurrentState == ButtonScript.ButtonState.Up);



		if (isJumpDown) {
			if (!_firstJump && m_BottomTouched) {
				_firstJump = true;
				_firstJumpEnd = false;

				//m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, JumpSpeed);
				StartCoroutine (Jump (JumpSpeed));
			}
			else if(_firstJumpEnd && !_secondJump) {
				_secondJump = true;
				// second jump
				//m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, JumpSpeed);
				StartCoroutine (Jump (JumpSpeed));
			}

		}

		else if(isJumpUp) {
			_firstJump = false;
			_firstJumpEnd = true;

			if(m_RigidBody2D.velocity.y > MinJumpSpeed)
				StartCoroutine (Jump (MinJumpSpeed));
				//m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, MinJumpSpeed);

		}
	}

	void FixedUpdate () {
		float move = 0.0f;

		if (BSMoveLeft.CurrentState == ButtonScript.ButtonState.Down)
			move = -1.0f;
		else if (BSMoveRight.CurrentState == ButtonScript.ButtonState.Down)
			move = 1.0f;
		else
			move = Input.GetAxis ("Horizontal");

		if (!_weapon && (BSFireA.CurrentState == ButtonScript.ButtonState.Down || Input.GetAxis("Fire1")==1)) {
			_weapon = true;
			Weapon.SetActive (true);
			Invoke("ResetWeapon", WeaponCoolDown);
			/*GameObject shotInstance = (GameObject)Instantiate (ShootPrefab);
			shotInstance.GetComponent<Shot> ().moveVector = m_FacingRight ? new Vector2 (-ShootSpeed, 0) : new Vector2 (ShootSpeed, 0);
			shotInstance.transform.position = m_FacingRight ? new Vector3(this.transform.position.x-100, this.transform.position.y + 150, this.transform.position.z) : new Vector3(this.transform.position.x+100, this.transform.position.y + 150, this.transform.position.z);
			*/
		}

		_moveDeb = Mathf.Abs(move);

		// Evalute states
		m_BottomTouched = m_BottoBox.IsTouchingLayers (_wallsMask) || m_BottomLeft.IsTouchingLayers (_wallsMask) || m_BottomRight.IsTouchingLayers (_wallsMask);
		m_FrontTouched = /*_left_box.IsTouchingLayers (wallsMask) ||*/ m_RightBox.IsTouchingLayers (_wallsMask) || m_BottomRight.IsTouchingLayers (_wallsMask);

		if(m_BottomTouched) {
			_firstJump = false;
			_secondJump = false;
		}

		//Animation part
		_anim.SetFloat("velocityY", m_RigidBody2D.velocity.y);
		if(m_RigidBody2D.velocity.y < 0)
			_anim.SetBool ("jump", false);
		if(move != 0)
			_anim.SetBool(runHash, true);
		else
			_anim.SetBool(runHash, false);

        if (m_BottomTouched)
        {
            _anim.SetBool("grounded", true);
        }
        else {
            _anim.SetBool("grounded", false);
        }




		// classic move
		if (Mathf.Abs (move) > 0) {
			if (!m_FrontTouched) {
				m_RigidBody2D.velocity = new Vector2 (move * MoveSpeed, m_RigidBody2D.velocity.y);
			} else if (m_FacingRight && move > 0) {
				m_RigidBody2D.velocity = new Vector2 (move * MoveSpeed, m_RigidBody2D.velocity.y);
			} else if (!m_FacingRight && move < 0) {
				m_RigidBody2D.velocity = new Vector2 (move * MoveSpeed, m_RigidBody2D.velocity.y);
			}
		}

		if(m_RigidBody2D.velocity.x > 0  && m_FacingRight)
			Flip ();				
		else if (m_RigidBody2D.velocity.x  < 0 && !m_FacingRight)
			Flip ();
	}

	private IEnumerator Jump(float iJumpSpeed) {
		_anim.SetBool ("jump", true);
		yield return new WaitForSeconds(JumpDelay);
		m_RigidBody2D.velocity = new Vector2 (m_RigidBody2D.velocity.x, iJumpSpeed);
	}

	void Flip() {
		m_FacingRight = !m_FacingRight;
		Vector3 lScale = transform.localScale;
		lScale.x *= -1;
		transform.localScale = lScale;
	}

	void ResetWeapon()
	{
		Weapon.SetActive (false);
		_weapon = false;
	}

	public void Hit (float iDamageValue){
		Health -= iDamageValue;
		PlayerHit _playerHitEvent = new PlayerHit(Health);
		_anim.SetTrigger(hitHash);
		Events.instance.Raise(_playerHitEvent);
		if(Health <= 0){
			_anim.SetTrigger(deathHash);
			this.enabled = false;
		}
	}

    public void SetIdle(bool isIdle) {
        _anim.SetBool(idleHash,isIdle);
        if(isIdle == false)
            _idleTimer = 0;
    }

    void OnTriggerEnter2D(Collider2D other) {
		Enemy otherEnemy = other.gameObject.GetComponent<Enemy> ();
		if (otherEnemy != null) {
			Hit (otherEnemy.DamagePerHit);
		}

        CoinScript otherCoin = other.gameObject.GetComponent<CoinScript>();
        if (otherCoin != null)
        {
            otherCoin.CoinAnim.destroy();
        }
        
    }
		
}
