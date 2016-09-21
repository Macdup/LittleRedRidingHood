using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombManager : MonoBehaviour {

	public List<Bomb> BombList = new List<Bomb> ();

	// Use this for initialization
	void Start () {
		var list = this.GetComponentsInChildren<Bomb> (true);
		for (var i = 0; i < list.Length; i++) {
			BombList.Add (list[i]);
		}
	}
	
	// Update is called once per frames
	void Update () {
	
	}

	public Bomb getUsableBomb(){
		for (var i = 0; i < BombList.Count; i++) {
			if (BombList [i].gameObject.activeSelf == false)
				return BombList [i];
		}
		return null;
	}
}
