using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PlantScript : Enemy {

	public GameObject ShootPrefab;
	public float ShootSpeed = 10.0f;
	public float ShootCoolDown = 3f;

	private GameObject _Player;
	private bool _loaded = false;
	private float _shootTimer = 0.0f;
	private float _detectionDistance = 200;

	int shotHash = Animator.StringToHash("shot");



	// Use this for initialization
	public override void Start () {
		base.Start();
		_Player = GameObject.Find("Player");
	}

	public override void Update(){

		base.Update();

		if(_loaded == false)
			_shootTimer += Time.deltaTime;

		if(_shootTimer > ShootCoolDown){
			_loaded = true;
			_shootTimer = 0;
		}
			
		float distancePlayer = Vector3.Distance(_Player.transform.position,transform.position);

		if(_loaded == true && distancePlayer <= _detectionDistance){
			startShot();
			_loaded = false;
		}

		base.Update();
			
	}

	void startShot() {
		if (!m_Dead) {
		m_Animator.SetTrigger(shotHash);
		}
	}

	public void shot() {
		if (!m_Dead) {
			GameObject shotInstance = (GameObject)Instantiate (ShootPrefab);
			shotInstance.GetComponent<Shot> ().Source = this.gameObject;
			shotInstance.GetComponent<Shot> ().moveVector = new Vector2 (ShootSpeed * transform.localScale.x * -1, 0);
			shotInstance.transform.position = new Vector3 (this.transform.position.x + (transform.localScale.x * (-20)), this.transform.position.y, this.transform.position.z);
		}
	}

}
