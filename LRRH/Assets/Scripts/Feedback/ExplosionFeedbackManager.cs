using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionFeedbackManager : MonoBehaviour {

	public List<ExplosionEffect> ExplosionEffectList = new List<ExplosionEffect> ();

	// Use this for initialization
	void Start () {
		var list = this.GetComponentsInChildren<ExplosionEffect> (true);
		for (var i = 0; i < list.Length; i++) {
			ExplosionEffectList.Add (list[i]);
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public ExplosionEffect getUsableExplosionEffect(){
		for (var i = 0; i < ExplosionEffectList.Count; i++) {
			if (ExplosionEffectList [i].gameObject.activeSelf == false)
				return ExplosionEffectList [i];
		}
		return null;
	}
}
