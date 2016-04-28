﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class lifeBar : MonoBehaviour {

	public void OnEnable ()
	{
		Events.instance.AddListener<playerHit>(onPlayerHit);
	}

	int fullSize; 

	// Use this for initialization
	void Start () {
		fullSize = (int)this.GetComponent<RectTransform>().sizeDelta.x;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// Update is called once per frame
	void onPlayerHit (playerHit e) {
		updateBarSize(e.health);
	}

	void updateBarSize(int health){
		Vector2 actualSize = this.GetComponent<RectTransform>().sizeDelta;
		actualSize.x = health * fullSize / 100;
		this.GetComponent<RectTransform>().sizeDelta = actualSize;
	}

}
