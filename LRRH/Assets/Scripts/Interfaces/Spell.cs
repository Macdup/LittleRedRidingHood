using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    // public member
    public float ManaCost = 10.0f;


    [HideInInspector]
    public Player Player;
    [HideInInspector]
    public Animator Anim;

	// Use this for initialization
	void Start () {
        Player = GetComponent<Player>();
        Anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void launchSort()
    {

    }
}
