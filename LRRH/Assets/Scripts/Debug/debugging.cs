using UnityEngine;
using System.Collections;

public class debugging : MonoBehaviour {

    public bool getAllPower;
	public float timeScale;

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
		Time.timeScale = timeScale;
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = timeScale;
	}
}
