using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Grenadier : Enemy {

	private BombManager m_BombManager;

	public override void Start(){
		base.Start();
		m_BombManager = GameObject.Find("BombManager").GetComponent<BombManager>();	
		Invoke ("throwGrenade", 1);
	}

    public void throwGrenade() {
		Bomb bomb = m_BombManager.getUsableBomb ();
		Vector3 popPosition = transform.position;
		popPosition.y += 20;
		bomb.pop (popPosition);
		Vector3 playerDir = (m_Player.transform.position - transform.position).normalized;
		playerDir.x = playerDir.x * 300;
		playerDir.y = playerDir.y * 100000;
		bomb.GetComponent<Rigidbody2D> ().velocity = playerDir;
    }
	
}
