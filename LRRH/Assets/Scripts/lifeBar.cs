using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lifeBar : MonoBehaviour {

	public void OnEnable ()
	{
		Events.instance.AddListener<PlayerHit>(onPlayerHit);
	}

	int fullSize; 

	// Use this for initialization
	void Start () {
		fullSize = (int)this.GetComponent<RectTransform>().sizeDelta.x;
        Debug.Log(fullSize); 
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Update is called once per frame
	void onPlayerHit (PlayerHit e) {
		UpdateBarSize(e.Health);
	}

	void UpdateBarSize(float iHealth){
		Vector2 actualSize = this.GetComponent<RectTransform>().sizeDelta;
        Debug.Log(actualSize);
        Debug.Log(iHealth);
		actualSize.x = iHealth * fullSize / 100;
		this.GetComponent<RectTransform>().sizeDelta = actualSize;
	}

}
