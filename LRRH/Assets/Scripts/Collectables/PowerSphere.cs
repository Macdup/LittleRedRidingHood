using UnityEngine;
using System.Collections;

public class PowerSphere : MonoBehaviour {

	public enum Power {
		DoubleJump, 
		ChargedAttack, 
		Counter,
		Jetpack
	};

	public Power power;
	private Player _player;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject == _player.gameObject) {
			switch (power) {
			case Power.DoubleJump:
				Debug.Log (_player);
				_player.setDoubleJump (true);
				break;
			case Power.ChargedAttack:
				_player.setChargedAttack (true);
				break;
			case Power.Counter:
				_player.setCounter (true);
				break;
			case Power.Jetpack:
				_player.setJetPack (true);
				break;
			}
			gameObject.SetActive (false);
		}
	}
}
