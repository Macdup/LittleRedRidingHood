using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitFeedbackManager : MonoBehaviour {

	public List<HitPointEffect> HitPointEffectList = new List<HitPointEffect> ();

	// Use this for initialization
	void Start () {
		var list = this.GetComponentsInChildren<HitPointEffect> (true);
		for (var i = 0; i < list.Length; i++) {
			HitPointEffectList.Add (list[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public HitPointEffect getUsableHitPointEffect(){
		for (var i = 0; i < HitPointEffectList.Count; i++) {
			if (HitPointEffectList [i].gameObject.activeSelf == false)
				return HitPointEffectList [i];
		}
		return null;
	}
}
