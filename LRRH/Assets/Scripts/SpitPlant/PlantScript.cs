using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PlantScript : Enemy {

	public GameObject ShootPrefab;
	public float ShootSpeed = 10.0f;
	public float ShootCoolDown = 3f;

	private GameObject _Player;
	private bool _loaded = true;
	private float _shootTimer = 0.0f;
	//private float _detectionDistance = 200;
	private BoxCollider2D m_PlantZone;

	int shotHash = Animator.StringToHash("shot");



	// Use this for initialization
	public override void Start () {
		base.Start();
		m_PlantZone = this.GetComponentsInChildren<BoxCollider2D> ()[1];
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
			
		bool isInZone = m_PlantZone.OverlapPoint(new Vector2(_Player.transform.position.x, _Player.transform.position.y));

		if(_loaded && isInZone){
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
            Shot Shot = shotInstance.GetComponent<Shot>();
            Shot.Source = this.gameObject;
            shotInstance.transform.position = new Vector3(this.transform.position.x + (transform.localScale.x * (-20)), this.transform.position.y, this.transform.position.z);

            Quaternion rotation = Quaternion.LookRotation
            (_Player.transform.position - shotInstance.transform.position, shotInstance.transform.TransformDirection(Vector3.up));
            shotInstance.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

            Vector3 moveVector = _Player.transform.position - transform.position;
            Shot.MoveVector = moveVector.normalized * ShootSpeed;


		}
	}

}
