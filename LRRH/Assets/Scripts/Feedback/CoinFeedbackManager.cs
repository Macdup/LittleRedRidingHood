using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinFeedbackManager : MonoBehaviour {

	public List<CoinEffect> CoinFeedbackList = new List<CoinEffect> ();

	// Use this for initialization
	void Start () {
		var list = this.GetComponentsInChildren<CoinEffect> (true);
		for (var i = 0; i < list.Length; i++) {
			CoinFeedbackList.Add (list[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public CoinEffect getUsableCoinFeedback(){
		for (var i = 0; i < CoinFeedbackList.Count; i++) {
			if (CoinFeedbackList [i].gameObject.activeSelf == false)
				return CoinFeedbackList [i];
		}
		return null;
	}
}
