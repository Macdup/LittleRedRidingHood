using UnityEngine;
using System.Collections;

public class debugging : MonoBehaviour {

    public bool getAllPower;

    Player player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
        if (getAllPower == true) {
            player.setChargedAttack(true);
            player.setCounter(true);
            player.setDoubleJump(true);
            player.setJetPack(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
