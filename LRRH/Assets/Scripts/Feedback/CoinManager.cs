using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour {

	public List<Coin> CoinList = new List<Coin> ();

	// Use this for initialization
	void Start () {
		var list = this.GetComponentsInChildren<Coin> (true);
		for (var i = 0; i < list.Length; i++) {
			CoinList.Add (list[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Coin getUsableCoin(){
		for (var i = 0; i < CoinList.Count; i++) {
			if (CoinList [i].gameObject.activeSelf == false)
				return CoinList [i];
		}
		return null;
	}
}
