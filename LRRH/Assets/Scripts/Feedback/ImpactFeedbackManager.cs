using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImpactFeedbackManager : MonoBehaviour {

	public List<ImpactFeedback> ImpactFeedbackList = new List<ImpactFeedback> ();

	// Use this for initialization
	void Start () {
		var list = this.GetComponentsInChildren<ImpactFeedback> (true);
		for (var i = 0; i < list.Length; i++) {
			ImpactFeedbackList.Add (list[i]);
		}
	}
	
	// Update is called once per frames
	void Update () {
	
	}

	public ImpactFeedback getUsableImpact(){
		for (var i = 0; i < ImpactFeedbackList.Count; i++) {
			if (ImpactFeedbackList [i].gameObject.activeSelf == false)
				return ImpactFeedbackList [i];
		}
		return null;
	}
}
