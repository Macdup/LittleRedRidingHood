using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class playerHit : GameEvent{
	public int health;
	public playerHit(int thisHealth){
		health = thisHealth;
	}
}

public class Player : MonoBehaviour {
	
	public float MoveSpeed = 800.0f;
	public float JumpSpeed = 1000.0f;
	public float MinJumpSpeed = 200.0f;

	LayerMask wallsMask;

	public float TouchDetectionRadius = 0.2f;

	Animator _anim;
	int runHash = Animator.StringToHash("run");
	int jumpHash = Animator.StringToHash("jump");
	int hitHash = Animator.StringToHash("hit");
	int deathHash = Animator.StringToHash("death");
	int groundedHash = Animator.StringToHash("grounded");
    int idleHash = Animator.StringToHash("idle");
	public int Health = 100;


	private bool _facingRight = false;
	private bool _bottomTouched =  false;
	private bool _frontTouched =  false;

	private bool _firstJump =  false;
	private bool _firstJumpEnd =  false;
	private bool _secondJump =  false;

	private bool _shoot = false;

	private Rigidbody2D _rigidBody2D;

	private BoxCollider2D _bottom_box;
	private CircleCollider2D _bottom_left;
	private CircleCollider2D _bottom_right;
	private BoxCollider2D _left_box;
	private BoxCollider2D _right_box;

	public ButtonScript BSMoveLeft;
	public ButtonScript BSMoveRight;
	public ButtonScript BSJump;
	public ButtonScript BSFireA;
	public ButtonScript BSFireB;   

	public GameObject ShootPrefab;
	public float ShootSpeed = 1000.0f;
	public float ShootCoolDown = 0.3f;

    public float idleTimer = 0.0f;


//	public Button ButtonLeft;
//	public Button ButtonRight;
//	public Button ButtonJump;
//	private Rect _touchMoveLeft = new Rect(0, 0, Screen.width / 3, 2*Screen.height / 3);
//	private Rect _touchMoveRight = new Rect(0, 0, Screen.width / 3, 2*Screen.height / 3);
//	private Rect _touchJump = new Rect(0, 0, Screen.width / 3, 2*Screen.height / 3);
//	private Rect _touchFireA = new Rect(0, 0, Screen.width / 3, 2*Screen.height / 3);

	// Use this for initialization
	void Start () {
		_anim = GetComponentInChildren<Animator>();
		_rigidBody2D = GetComponent<Rigidbody2D> ();
		wallsMask = LayerMask.GetMask("Walls");

		_bottom_box = GetComponents<BoxCollider2D> () [1];
		_bottom_left = GetComponents<CircleCollider2D> () [0];
		_bottom_right = GetComponents<CircleCollider2D> () [1];
		_left_box = GetComponents<BoxCollider2D> () [2];
		_right_box = GetComponents<BoxCollider2D> () [3];

//		BSMoveLeft = ButtonState.None;
//		BSMoveRight = ButtonState.None;
//		BSJump = ButtonState.None;
//		BSFireA = ButtonState.None;
//		BSFireB = ButtonState.None;


//		_touchMoveLeft = new Rect (
//			ButtonLeft.rectTransform.anchorMin.x*Screen.width + 	touchButtonLeft.rectTransform.anchoredPosition.x - touchButtonLeft.rectTransform.rect.width / 2,
//			ButtonLeft.rectTransform.anchorMin.y*Screen.height + 	touchButtonLeft.rectTransform.anchoredPosition.y - touchButtonLeft.rectTransform.rect.height / 2,
//			ButtonLeft.rectTransform.anchorMin.x*Screen.width + 	touchButtonLeft.rectTransform.anchoredPosition.x + touchButtonLeft.rectTransform.rect.width / 2,
//			ButtonLeft.rectTransform.anchorMin.y*Screen.height + 	touchButtonLeft.rectTransform.anchoredPosition.y + touchButtonLeft.rectTransform.rect.height / 2
//		);
//		_touchMoveRight = new Rect (
//			touchButtonRight.rectTransform.anchorMin.x*Screen.width + touchButtonRight.rectTransform.anchoredPosition.x - touchButtonRight.rectTransform.rect.width / 2,
//			touchButtonRight.rectTransform.anchorMin.y*Screen.height + touchButtonRight.rectTransform.anchoredPosition.y - touchButtonRight.rectTransform.rect.height / 2, 
//			touchButtonRight.rectTransform.anchorMin.x*Screen.width + touchButtonRight.rectTransform.anchoredPosition.x + touchButtonRight.rectTransform.rect.width / 2,
//			touchButtonRight.rectTransform.anchorMin.y*Screen.height + touchButtonRight.rectTransform.anchoredPosition.y + touchButtonRight.rectTransform.rect.height / 2
//		);
//		_touchJump = new Rect (
//			touchButtonJump.rectTransform.anchorMin.x*Screen.width + touchButtonJump.rectTransform.anchoredPosition.x - touchButtonJump.rectTransform.rect.width / 2,
//			touchButtonJump.rectTransform.anchorMin.y*Screen.height + touchButtonJump.rectTransform.anchoredPosition.y - touchButtonJump.rectTransform.rect.height / 2,
//			touchButtonJump.rectTransform.anchorMin.x*Screen.width + touchButtonJump.rectTransform.anchoredPosition.x + touchButtonJump.rectTransform.rect.width / 2,
//			touchButtonJump.rectTransform.anchorMin.y*Screen.height + touchButtonJump.rectTransform.anchoredPosition.y + touchButtonJump.rectTransform.rect.height / 2
//		);
	}



	//private bool _wasJumpDown = false;

	void Update() {
		bool isJumpDown = false;
		bool isJumpUp = false;

        //Idle timer
        idleTimer += Time.deltaTime;

        if (_rigidBody2D.velocity.x != 0 || _rigidBody2D.velocity.y != 0)
        {
            setIdle(false);
        }

        if (idleTimer >= 2) {
            setIdle(true);
        }

//		if (Input.touchCount > 0) {
//			bool foundJump = false;
//			foreach (Touch t in Input.touches) {
//				if (_touchJump.Contains (t.position)) {
//					isJumpDown = true;
//					_wasJumpDown = true;
//					foundJump = true;
//				}
//			}
//
//			if (!foundJump && _wasJumpDown) {
//				_wasJumpDown = false;
//				isJumpUp = true;
//			}
//
//		} else if (_wasJumpDown) {
//			_wasJumpDown = false;
//			isJumpUp = true;
//		} else {
//			isJumpDown = Input.GetButtonDown ("Jump");
//			isJumpUp = Input.GetButtonUp ("Jump");
//		}

		isJumpDown = Input.GetButtonDown ("Jump") || (BSJump.CurrentState == ButtonScript.ButtonState.Down);
		isJumpUp = Input.GetButtonUp ("Jump") || (BSJump.CurrentState == ButtonScript.ButtonState.Up);



		if (isJumpDown) {
			if (!_firstJump && _bottomTouched) {
				_firstJump = true;
				_firstJumpEnd = false;
				_rigidBody2D.velocity = new Vector2 (_rigidBody2D.velocity.x, JumpSpeed);
			}
			else if(_firstJumpEnd && !_secondJump) {
				_secondJump = true;
				// second jump
				_rigidBody2D.velocity = new Vector2 (_rigidBody2D.velocity.x, JumpSpeed);
			}

		}

		else if(isJumpUp) {
			_firstJump = false;
			_firstJumpEnd = true;

			if(_rigidBody2D.velocity.y > MinJumpSpeed)
				_rigidBody2D.velocity = new Vector2 (_rigidBody2D.velocity.x, MinJumpSpeed);
		}
	}

	void FixedUpdate () {
		float move = 0.0f;

//		if(Input.touchCount > 0) {
//			foreach (Touch t in Input.touches) {
//				if (_touchMoveLeft.Contains (t.position)) {
//					move = -1.0f;
//				} else if (_touchMoveRight.Contains (t.position)) {
//					move = 1.0f;
//				}
//			}
//		}
//		else {
//			move = Input.GetAxis ("Horizontal");
//		}

		if (BSMoveLeft.CurrentState == ButtonScript.ButtonState.Down)
			move = -1.0f;
		else if (BSMoveRight.CurrentState == ButtonScript.ButtonState.Down)
			move = 1.0f;
		else
			move = Input.GetAxis ("Horizontal");

		if (!_shoot && (BSFireA.CurrentState == ButtonScript.ButtonState.Down /*|| Input.GetAxis("Fire1")==1*/)) {
			_shoot = true;
			GameObject shotInstance = (GameObject)Instantiate (ShootPrefab);
			shotInstance.GetComponent<Shot> ().moveVector = _facingRight ? new Vector2 (-ShootSpeed, 0) : new Vector2 (ShootSpeed, 0);
			shotInstance.transform.position = _facingRight ? new Vector3(this.transform.position.x-100, this.transform.position.y + 150, this.transform.position.z) : new Vector3(this.transform.position.x+100, this.transform.position.y + 150, this.transform.position.z);
			Invoke("ResetShoot", ShootCoolDown);
		}

		moveDeb = Mathf.Abs(move);

		// Evalute states
		_bottomTouched = _bottom_box.IsTouchingLayers (wallsMask) || _bottom_left.IsTouchingLayers (wallsMask) || _bottom_right.IsTouchingLayers (wallsMask);
		_frontTouched = /*_left_box.IsTouchingLayers (wallsMask) ||*/ _right_box.IsTouchingLayers (wallsMask) || _bottom_right.IsTouchingLayers (wallsMask);

		if(_bottomTouched) {
			_firstJump = false;
			_secondJump = false;
		}

		//Animation part
		_anim.SetFloat("velocityY", _rigidBody2D.velocity.y);		
		if(move != 0)
			_anim.SetBool(runHash, true);
		else
			_anim.SetBool(runHash, false);

        if (_bottomTouched)
        {
            _anim.SetBool("grounded", true);
        }
        else {
            _anim.SetBool("grounded", false);
        }




		// classic move
		if(Mathf.Abs(move) > 0)
			_rigidBody2D.velocity = new Vector2 (move * MoveSpeed, _rigidBody2D.velocity.y);

		if(_rigidBody2D.velocity.x > 0  && _facingRight)
			Flip ();				
		else if (_rigidBody2D.velocity.x  < 0 && !_facingRight)
			Flip ();
	}

	void Flip() {
		_facingRight = !_facingRight;
		Vector3 lScale = transform.localScale;
		lScale.x *= -1;
		transform.localScale = lScale;
	}

	void ResetShoot()
	{
		_shoot = false;
	}

	public void Hit (int hitValue){
		Health -= hitValue;
		playerHit _playerHitEvent = new playerHit(Health);
		_anim.SetTrigger(hitHash);
		Events.instance.Raise(_playerHitEvent);
		if(Health <= 0){
			_anim.SetTrigger(deathHash);
			this.enabled = false;
		}
	}


	public bool frontHitOn = false;
	public float moveDeb;

    public void setIdle(bool isIdle) {
        _anim.SetBool(idleHash,isIdle);
        if(isIdle == false)
            idleTimer = 0;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Hit(20);
    }
		
}
