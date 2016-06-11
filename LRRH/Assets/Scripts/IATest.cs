using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class IATest : Enemy
{
    // private member
    private bool hasnotdetected;
    private bool hasDetected;
    private bool isAttacking;

    Animator _anim;
    int runHash = Animator.StringToHash("run");
    int hitHash = Animator.StringToHash("hit");
    int deathHash = Animator.StringToHash("death");
    int idleHash = Animator.StringToHash("idle");
    int _DefendHash = Animator.StringToHash("defend");
    int _GroggyHash = Animator.StringToHash("isGroggy");

	// Use this for initialization
	void Start () {
        hasnotdetected = true;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
