﻿using UnityEngine;
using System.Collections;

public class Screen : MonoBehaviour {

    public Vector2 minBound;
    public Vector2 maxBound;
	public Vector2 centerPos;
	public enum Zone // your custom enumeration
	{
		ForestGround,
		CaveGround,
		ForestCaveGround
	};
		
	public Zone zone;

}
