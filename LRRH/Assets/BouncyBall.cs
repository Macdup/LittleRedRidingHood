﻿using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class BouncyBall : Enemy {

    public Vector2 InitialDirection = new Vector2(-50, 0);


    private Rigidbody2D _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = this.GetComponent<Rigidbody2D>();

        _rigidbody.velocity = InitialDirection;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
